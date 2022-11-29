using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Universe.EntityFrameworkCore.UnitTest
{
    public class EntityA : Entity
    {
        public string? Name { get; set; }
    }
    public class EntityB : Entity
    {
        public string? Name { get; set; }
        public long EntityAId { get; set; }
    }

    public class EntityAEntityTypeConfiguration : IEntityTypeConfiguration<EntityA>
    {
        public void Configure(EntityTypeBuilder<EntityA> builder)
        {
            builder.ToTable("EntityAs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(32);
        }
    }
    public class EntityBEntityTypeConfiguration : IEntityTypeConfiguration<EntityB>
    {
        public void Configure(EntityTypeBuilder<EntityB> builder)
        {
            builder.ToTable("EntityBs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(32);
            builder.Property(x => x.EntityAId);
        }
    }
}
