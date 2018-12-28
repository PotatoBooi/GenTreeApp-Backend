using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenTreeApp.API.Persistence.Configurations
{
    class DetailsConfiguration : IEntityTypeConfiguration<Details>
    {
        public void Configure(EntityTypeBuilder<Details> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(n => n.Name).HasMaxLength(50).IsRequired();
            builder.Property(sn => sn.Surname).HasMaxLength(50).IsRequired();
            builder.Property(s => s.Sex).IsRequired();
//            builder.HasOne(p => p.Person).WithOne(d => d.Details).IsRequired();


        }
    }
}
