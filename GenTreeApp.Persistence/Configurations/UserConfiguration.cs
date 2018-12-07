using System;
using System.Collections.Generic;
using System.Text;
using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenTreeApp.Persistence.Configurations
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
