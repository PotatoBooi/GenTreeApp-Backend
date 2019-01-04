using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenTreeApp.API.Persistence.Configurations
{
    class RelationConfiguration:IEntityTypeConfiguration<Relation>
    {
        public void Configure(EntityTypeBuilder<Relation> builder)
        {
            builder.HasKey(ut => new { ut.PersonId, ut.SecondPersonId });
            builder.Property(t => t.Type).IsRequired();
            builder.HasOne(p => p.Person).WithMany(r => r.Relations1).HasForeignKey(p=>p.PersonId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.SecondPerson).WithMany(r => r.Relations2).HasForeignKey(s=>s.SecondPersonId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
