namespace MoviesAPI.DTOs
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public required string StoryLine { get; set; }

        public byte[]? Poster { get; set; }

        public int GenreId { get; set; }  // Foreign key

        public string? GenreName { get; set; }
    }
}
