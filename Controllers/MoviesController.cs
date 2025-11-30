using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly List<string> _allowedExtensions = new List<string> {".jpg", ".png"};
        private long _maxAllowedPosterSize =  1024 * 1024; // 1MB

        // Constructor to inject the ApplicationDbContext
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _context.Movies
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre) // include genres to view the genre details along with movies
                .ToListAsync();

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound($"No movie was found with id: {id}!");
            else
                return Ok(movie);
        }

        [HttpGet("GetByGenre/{genreId}")]
        public async Task<IActionResult> GetByGenreIdAsync(int genreId)
        {
            var movies = await _context.Movies
                .Where(m => m.GenreId == genreId)
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre)
                .ToListAsync();

            if (movies.Count == 0)
                return NotFound($"No movies were found for genre id: {genreId}!");
            else
                return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateMovieDto dto)
        {
            using var dataStream = new MemoryStream();
            if (dto.Poster is not null)
            {
                if(!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()) == false)
                    return BadRequest("Only .jpg and .png images are allowed!");
                if (dto.Poster.Length > 5 * 1024 * 1024)
                    return BadRequest("Poster size must not exceed 5MB.");
                else
                    await dto.Poster.CopyToAsync(dataStream); // Copy the poster file to the memory stream
            }

            var Poster = dto.Poster is null ? null : dataStream.ToArray();

            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Rate = dto.Rate,
                StoryLine = dto.StoryLine,
                Poster = Poster,
                GenreId = dto.GenreId
            };

            var isValidGenre = await _context.Genres.AnyAsync(g => g.Id == dto.GenreId);

            if (!isValidGenre)
                return NotFound($"There is no genre with id {dto.GenreId}");

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return Ok(movie);
        }
    }
}
