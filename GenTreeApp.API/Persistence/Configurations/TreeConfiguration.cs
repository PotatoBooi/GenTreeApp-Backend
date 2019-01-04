using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenTreeApp.API.Persistence.Configurations
{
    class TreeConfiguration:IEntityTypeConfiguration<Tree>
    {
        public void Configure(EntityTypeBuilder<Tree> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(n => n.Name).IsRequired();
            builder.Property(e => e.Editable).IsRequired();
            builder.HasMany(t => t.Persons).WithOne(t => t.Tree);

        }
    }
}
