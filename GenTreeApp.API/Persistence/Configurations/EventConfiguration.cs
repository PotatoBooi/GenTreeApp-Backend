using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenTreeApp.API.Persistence.Configurations
{
    class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(t => t.Type).IsRequired();
            builder.Property(d => d.Date).IsRequired();
        }
    }
}
