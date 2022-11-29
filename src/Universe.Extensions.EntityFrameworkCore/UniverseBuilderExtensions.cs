namespace Universe.Extensions.EntityFrameworkCore
{
    public static class UniverseBuilderExtensions
    {
        /// <summary>
        /// 添加EntityFrameworkCore扩展功能
        /// </summary> 
        /// <returns></returns>
        public static UniverseBuilder AddEntityFrameworkCore<TDbContext>(this UniverseBuilder builder, Action<DbContextOptionsBuilder> configure)
            where TDbContext : EfCoreDbContext<TDbContext>
        {
            // 查找所有的是实体类型
            var entityTypes = from property in typeof(TDbContext).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                              where
                                  ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>)) &&
                                  ReflectionHelper.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0],
                                      typeof(IEntity<>))
                              select property.PropertyType.GenericTypeArguments[0];
            // 注册仓储服务类及接口
            foreach (var entityType in entityTypes)
            {
                var keyType = EntityHelper.GetKeyType(entityType);
                if (keyType == typeof(long))
                    builder.Services.AddScoped(typeof(IRepository<>).MakeGenericType(entityType), typeof(EfCoreRepository<,>).MakeGenericType(typeof(TDbContext), entityType));
                else
                    builder.Services.AddScoped(typeof(IRepository<,>).MakeGenericType(entityType, keyType), typeof(EfCoreRepository<,,>).MakeGenericType(typeof(TDbContext), entityType, keyType));

            }
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(EfCoreUnitOfWork<>).MakeGenericType(typeof(TDbContext)));
            builder.Services.AddDbContext<TDbContext>(configure);
            return builder;
        }
    }
}
