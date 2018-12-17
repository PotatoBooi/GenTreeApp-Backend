using System;
using System.Collections.Generic;
using System.Text;
using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenTreeApp.Persistence.Configurations
{
    class RelationConfiguration:IEntityTypeConfiguration<Relation>
    {
        public void Configure(EntityTypeBuilder<Relation> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(t => t.Type).IsRequired();
            builder.HasOne(p => p.Person).WithMany(r => r.Relations1);
            builder.HasOne(p => p.SecondPerson).WithMany(r => r.Relations2);
        }
    }
}
