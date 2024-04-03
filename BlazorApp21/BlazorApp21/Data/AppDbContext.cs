using Microsoft.EntityFrameworkCore;
using Shared_Library.Models;
namespace BlazorApp21.Data
{
   
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
