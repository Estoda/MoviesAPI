namespace MoviesAPI.DTOs
{
    public class CreateMovieDto
    {
        [ MaxLength(250)]
        public required string Title { get; set; }

        public int Year {get; set;}

        public double Rate {get; set;}

        [MaxLength(2500)]
        public required string StoryLine {get; set;}

        public IFormFile? Poster {get; set;}

        public int GenreId {get; set;}  // Foreign key
    }
}
