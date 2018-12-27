using System;
using System.Collections.Generic;
using System.Text;
using GenTreeApp.Domain.Entities;
using GenTreeApp.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GenTreeApp.Persistence
{
    public class TreeDbContext : DbContext
    {

        public DbSet<Tree> Trees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Details> Details { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Person> Persons { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new TreeConfiguration());
            modelBuilder.ApplyConfiguration(new RelationConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new MediaConfiguration());
            modelBuilder.ApplyConfiguration(new DetailsConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
