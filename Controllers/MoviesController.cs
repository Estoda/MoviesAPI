using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Services;
using AutoMapper;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly GetAllMoviesQuiry _getAllMoviesQuiry;
        private readonly List<string> _allowedExtensions = new List<string> { ".jpg", ".png" };
        private readonly long _maxAllowedPosterSize = 1024 * 1024; // 1MB

        // Constructor to inject the ApplicationDbContext
        public MoviesController(GetAllMoviesQuiry getAllMoviesQuiry)
        {
            _getAllMoviesQuiry = getAllMoviesQuiry;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _getAllMoviesQuiry.Handle();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id,[FromServices] GetByIdQuery getByIdQuery)
        {
            var movie = await getByIdQuery.Execute(id);

            if (movie == null)
                return NotFound($"No movie was found with id: {id}!");
            else
                return Ok(movie);
        }

        // [HttpGet("GetByGenre/{genreId}")]
        // public async Task<IActionResult> GetByGenreIdAsync(int genreId)
        // {
        //     var movies = await _moviesService.GetByGenreId(genreId);

        //     if (!movies.Any())
        //         return NotFound($"No movies were found for genre id: {genreId}!");
        //     else
        //         return Ok(movies);
        // }

        // [HttpPost]
        // public async Task<IActionResult> CreateAsync(CreateMovieDto dto)
        // {
        //     using var dataStream = new MemoryStream();
        //     if (dto.Poster is not null)
        //     {
        //         if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
        //             return BadRequest("Only .jpg and .png images are allowed!");
        //         if (dto.Poster.Length > _maxAllowedPosterSize)
        //             return BadRequest("Poster size must not exceed 5MB.");
        //         else
        //             await dto.Poster.CopyToAsync(dataStream); // Copy the poster file to the memory stream
        //     }

        //     var movie = _mapper.Map<Movie>(dto);

        //     movie.Poster = dataStream.ToArray();

        //     var isValidGenre = await _moviesService.IsValidGenre(dto.GenreId);

        //     if (!isValidGenre)
        //         return NotFound($"There is no genre with id {dto.GenreId}");

        //     await _moviesService.Add(movie);

        //     return Ok(movie);
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateAsync(int id,[FromForm] CreateMovieDto dto)
        // {
        //     var movie = (await _moviesService.GetAll(id)).FirstOrDefault();

        //     if (movie == null)
        //         return NotFound($"No movie was found with id: {id}!");
        //     else if (dto == null)
        //         return BadRequest("Invalid movie data.");
        //     else if (!await _moviesService.IsValidGenre(movie.GenreId))
        //         return NotFound($"There is no genre with id {dto.GenreId}");
        //     else
        //     {
        //         using var dataStream = new MemoryStream();
        //         if (dto.Poster is not null)
        //         {
        //             if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
        //                 return BadRequest("Only .jpg and .png images are allowed!");
        //             if (dto.Poster.Length > 5 * 1024 * 1024)
        //                 return BadRequest("Poster size must not exceed 5MB.");
        //             else
        //                 await dto.Poster.CopyToAsync(dataStream); // Copy the poster file to the memory stream
        //         }

        //         var Poster = dto.Poster is null ? movie.Poster : dataStream.ToArray();

        //         movie.Title = dto.Title;
        //         movie.GenreId = dto.GenreId;
        //         movie.Year = dto.Year;
        //         movie.Rate = dto.Rate;
        //         movie.StoryLine = dto.StoryLine;
        //         movie.Poster = Poster;

        //         _moviesService.Update(movie);
        //     }

        //     return Ok(movie);
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteAsync(int id)
        // {
        //     var movie = (await _moviesService.GetAll(id)).FirstOrDefault();

        //     if (movie == null)
        //         return NotFound($"No movie was found with id: {id}!");
        //     else
        //     {
        //         _moviesService.Delete(movie);
        //         return Ok(movie);
        //     }
        // }
    }
}
