using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

         public DbSet<Genre> Genres { get; set; } // DbSet for Genres Table
         public DbSet<Movie> Movies {get; set;} // DbSet for Movies Table
    }
}
