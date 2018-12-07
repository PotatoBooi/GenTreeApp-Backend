using System;
using System.Collections.Generic;
using System.Text;
using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenTreeApp.Persistence
{
    public class TreeDbContext : DbContext
    {

        public virtual DbSet<Tree> Trees { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Relation> Relations { get; set; }
        public virtual DbSet<Details> Details { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Person> Persons { get; set; }

    }
}
