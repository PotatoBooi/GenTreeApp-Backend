using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenTreeApp.API.Persistence.Configurations
{
    public class UserTreeConfiguration : IEntityTypeConfiguration<UserTree>
    {
        //join table for m-n for users and trees
        public void Configure(EntityTypeBuilder<UserTree> builder)
        {
            builder.HasKey(ut => new {ut.UserId, ut.TreeId});
            builder.HasOne(ut => ut.User).WithMany(u => u.UserTrees).HasForeignKey(ut=>ut.UserId);
            builder.HasOne(ut => ut.Tree).WithMany(u => u.UserTrees).HasForeignKey(ut=>ut.TreeId);

        }
    }
}
