namespace CommentsApi.Data;

using Microsoft.EntityFrameworkCore;
using CommentsApi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Comment> Comments => Set<Comment>();
}
