﻿using System;
using System.Collections.Generic;
using System.Text;
using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenTreeApp.Persistence.Configurations
{
    class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(t => t.Type).IsRequired();
            builder.Property(u => u.Url).IsRequired();
        }
    }
}
