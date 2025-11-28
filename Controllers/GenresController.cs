using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        // Constructor to inject the ApplicationDbContext
        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var geres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            return Ok(geres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, CreateGenreDto dto)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if (genre == null)
                return NotFound($"No genre was found with id: {id}!");
            else
            {
                genre.Name = dto.Name;
                await _context.SaveChangesAsync();
                return Ok(genre);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if (genre == null)
                return NotFound($"No genre was found with id: {id}!");
            else
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
                return Ok(genre);
            }
        }

    }
}
