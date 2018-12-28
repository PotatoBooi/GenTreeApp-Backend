using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenTreeApp.API.Persistence.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(n => n.Name).IsRequired().HasMaxLength(20);
            builder.Property(ph => ph.PasswordHash).IsRequired();
            builder.Property(ph => ph.PasswordSalt).IsRequired();

      


        }
    }
}
