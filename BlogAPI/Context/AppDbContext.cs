using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Context
{
  public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
  {
    public DbSet<Blog> Blogs { get; set; }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      modelBuilder.Entity<Tag>()
      .HasIndex(t => t.Name)
      .IsUnique();

      modelBuilder.Entity<Blog>()
          .HasOne(b => b.Author)
          .WithMany()
          .HasForeignKey(b => b.AuthorId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<Blog>()
          .HasMany(b => b.Tags)
          .WithMany(t => t.Blogs)
          .UsingEntity(j => j.ToTable("BlogTags"));
        
    }
  }
}