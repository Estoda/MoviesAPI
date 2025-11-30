
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext _context;

        public GenresService(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChangesAsync();

            return genre;
        }

        public async Task<Genre?> GetById(int id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id); ;
        }

        public  Genre Delete(Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChangesAsync();

            return genre;
        }
    }
}
