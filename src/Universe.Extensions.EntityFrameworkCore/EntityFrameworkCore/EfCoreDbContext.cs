namespace Universe.Extensions.EntityFrameworkCore
{
    public abstract class EfCoreDbContext<TDbContext> : DbContext
        where TDbContext : DbContext
    {
        private readonly LoggerFactory _loggerFactory = new(new[] {
            new DebugLoggerProvider()
        });
        public EfCoreDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options)
        {
            UserIdentifier = serviceProvider.GetService<IUserIdentifier>();
            EventBus = serviceProvider.GetService<IEventBus>();
            EntityChangeEventHelper = serviceProvider.GetService<IEntityChangeEventHelper>();
        }
        protected string? Schema { get; }
        protected IUserIdentifier? UserIdentifier { get; }
        protected IEntityChangeEventHelper? EntityChangeEventHelper { get; private set; }
        protected IEventBus? EventBus { get; private set; }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ApplySetAuditedEntity();
            NotifyEntityEvents();
            int result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            NotifyDomainEvents();
            return result;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ApplySetAuditedEntity();
            NotifyEntityEvents();
            int result = base.SaveChanges(acceptAllChangesOnSuccess);
            NotifyDomainEvents();
            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Schema != null)
                modelBuilder.Model.SetDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TDbContext).Assembly);
            ApplyFilterDeletedEntity(modelBuilder);
        }

        #region Utilities
        /// <summary>
        /// 实体事件通知
        /// </summary>
        private void NotifyEntityEvents()
        {
            foreach (EntityEntry entry in ChangeTracker.Entries().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        EntityChangeEventHelper?.NotifyEntityCreatedEvent(entry.Entity);
                        break;
                    case EntityState.Modified:
                        EntityChangeEventHelper?.NotifyUpdatedEvent(entry.Entity);
                        break;
                    case EntityState.Deleted:
                        EntityChangeEventHelper?.NotifyEntityDeletedEvent(entry.Entity);
                        break;
                }
            }
        }
        /// <summary>
        /// 领域事件通知
        /// </summary>
        private void NotifyDomainEvents()
        {
            var aggregateRoots = ChangeTracker.Entries<IAggregateRoot>()
               .Select(e => e.Entity)
               .Where(e => e.DomainEvents != null)
               .ToArray();
            foreach (IAggregateRoot aggregateRoot in aggregateRoots)
                if (aggregateRoot.DomainEvents != null)
                    EventBus?.SendAsync(aggregateRoot.DomainEvents);
        }
        /// <summary>
        /// 设置审计数据默认值
        /// </summary>
        private void ApplySetAuditedEntity()
        {
            foreach (EntityEntry entry in ChangeTracker.Entries().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ApplySetCreationAuditedEntity(entry);
                        break;
                    case EntityState.Modified:
                        ApplySetModificationAuditedEntity(entry);
                        break;
                    case EntityState.Deleted:
                        ApplySetDeletionAuditedEntity(entry);
                        break;
                }
            }
        }

        private void ApplySetCreationAuditedEntity(EntityEntry entry)
        {
            if (entry.Entity is not IHasCreationTime hasCreation)
                return;
            hasCreation.CreatedOnUtc = DateTime.UtcNow;

            if (entry.Entity is not ICreationAudited creationAudited)
                return;
            if (UserIdentifier != null && UserIdentifier.UserId != null)
                creationAudited.CreatorUserId = UserIdentifier.UserId.Value;
        }
        private void ApplySetModificationAuditedEntity(EntityEntry entry)
        {
            if (entry.Entity is not IHasModificationTime hasModification)
                return;
            hasModification.LastModifiedOnUtc = DateTime.UtcNow;

            if (entry.Entity is not IModificationAudited modificationAudited)
                return;
            if (UserIdentifier != null && UserIdentifier.UserId.HasValue)
                modificationAudited.LastModifierUserId = UserIdentifier.UserId.Value;
        }
        private void ApplySetDeletionAuditedEntity(EntityEntry entry)
        {
            if (entry.Entity is not IHasDeletionTime hasDeletion)
                return;
            entry.Reload();
            entry.State = EntityState.Modified;

            hasDeletion.DeletedOnUtc = DateTime.UtcNow;
            hasDeletion.IsDeleted = true;

            if (entry.Entity is not IDeletionAudited deletionAudited)
                return;
            if (UserIdentifier != null && UserIdentifier.UserId.HasValue)
                deletionAudited.DeleterUserId = UserIdentifier.UserId.Value;
        }
        private void ApplyFilterDeletedEntity(ModelBuilder modelBuilder)
        {
            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(ISoftDelete).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType).Property<bool>("IsDeleted");
                ParameterExpression parameter = Expression.Parameter(entityType.ClrType, "e");
                BinaryExpression body = Expression.Equal(
                    Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("IsDeleted")),
                Expression.Constant(false));
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
            }
        }
        #endregion

    }
}