using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenTreeApp.API.Persistence.Configurations
{
    class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasOne(d => d.Details).WithOne(p => p.Person).HasForeignKey<Person>(f=>f.DetailsId).IsRequired();
            builder.HasMany(r => r.Relations1).WithOne(p => p.Person);
            builder.HasMany(r => r.Relations2).WithOne(p => p.SecondPerson);
        }
    }
}
