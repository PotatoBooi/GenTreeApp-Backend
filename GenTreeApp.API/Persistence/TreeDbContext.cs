using GenTreeApp.API.Persistence.Configurations;
using GenTreeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenTreeApp.API.Persistence
{
    public class TreeDbContext : DbContext
    {
        public TreeDbContext(DbContextOptions options):base(options)
        {

        }


        public DbSet<Tree> Trees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Details> Details { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<UserTree> UserTrees { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new TreeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTreeConfiguration());
            modelBuilder.ApplyConfiguration(new RelationConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new MediaConfiguration());
            modelBuilder.ApplyConfiguration(new DetailsConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
