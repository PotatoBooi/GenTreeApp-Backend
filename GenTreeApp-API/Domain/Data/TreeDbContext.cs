using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using GenTreeApp_API.Domain.Models;

namespace GenTreeApp_API.Domain.Data
{
    public class TreeDbContext : DbContext
    {
        // Your context has been configured to use a 'TreeDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'GenTreeApp_API.TreeDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TreeDbContext' 
        // connection string in the application configuration file.
        public TreeDbContext()
            : base("name=TreeDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Tree> Trees { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Relation> Relations { get; set; }
        public virtual DbSet<Details> Details { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Person> Persons { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //configuring primary keys
//            modelBuilder.Entity<Tree>().Property(t => t.UUID)
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().HasKey(u => u.UUID);
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(15);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Tree>().HasKey(u => u.UUID);
            modelBuilder.Entity<Person>().HasKey(u => u.UUID);
            modelBuilder.Entity<Relation>().HasKey(u => u.UUID);
            modelBuilder.Entity<Event>().HasKey(u => u.UUID);
            modelBuilder.Entity<Details>().HasKey(u => u.UUID);
            modelBuilder.Entity<Media>().HasKey(u => u.UUID);
            modelBuilder.Entity<Comment>().HasKey(u => u.UUID);
            

        }


    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}