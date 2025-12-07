
using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Services
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext _context;

        public MoviesRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Movie>> GetAll(int movieId = 0)
        {
            return await _context.Movies
                .Where(m => m.Id == movieId || movieId == 0)
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre) 
                .ToListAsync();
        }

        public async Task<Movie?> GetById(int movieId)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == movieId);
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> IsValidGenre(int genreId)
        {
            return await _context.Genres.AnyAsync(g => g.Id == genreId);
        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChangesAsync();

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetByGenreId(int genreId)
        {
            return await _context.Movies
                .Where(m => m.GenreId == genreId)
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre)
                .ToListAsync();
        }

        public Movie Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChangesAsync();

            return movie;
        }
    }
}
