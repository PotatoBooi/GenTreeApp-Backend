using System;
using System.Collections.Generic;
using System.Text;
using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenTreeApp.Persistence.Configurations
{
    class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasOne(d => d.Details).WithOne(p => p.Person).IsRequired();
            builder.HasMany(r => r.Relations1).WithOne(p => p.Person);
            builder.HasMany(r => r.Relations2).WithOne(p => p.SecondPerson);
        }
    }
}
