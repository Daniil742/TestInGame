using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TestInGame.Data.Entities;

namespace TestInGame.Data
{
    public class InGameDbContext : DbContext
    {
        public InGameDbContext(DbContextOptions<InGameDbContext> options)
            : base(options) => DbInitializer.Initialize(this);

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Author>()
                .HasMany(a => a.Books)
                .WithMany(b => b.Authors)
                .UsingEntity(j => j.ToTable("AuthorBook"));
            base.OnModelCreating(modelBuilder);
        }
    }
}
