using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Services;



namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;
        // Constructor to inject the ApplicationDbContext
        public GenresController(IGenresService genresService) => _genresService = genresService;


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genresService.GetAll();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was found with id: {id}!");
            else
                return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };

            await _genresService.Add(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAsync(int id, CreateGenreDto dto)
        {
            var genre = _genresService.GetById(id).Result;

            if (genre == null)
                return NotFound($"No genre was found with id: {id}!");
            else if (dto == null)
                return BadRequest("Genre data is null!");
            else if (_genresService.GetAll().Result.Any(g => g.Name.ToLower() == dto.Name.ToLower() && g.Id != id))
                return BadRequest($"Genre with name '{dto.Name}' already exists!");
            else
            {
                genre.Name = dto.Name;
                _genresService.Update(genre);
                return Ok(genre);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was found with id: {id}!");
            else
            {
                _genresService.Delete(genre);
                return Ok(genre);
            }
        }

    }
}
