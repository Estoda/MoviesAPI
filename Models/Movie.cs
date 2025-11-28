namespace MoviesAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [ MaxLength(250)]
        public required string Title { get; set; }

        public int Year {get; set;}

        public double Rate {get; set;}

        [MaxLength(2500)]
        public required string StoryLine {get; set;}

        public byte[]? Poster {get; set;}

        public int GenreId {get; set;}  // Foreign key

        public Genre? Genre {get; set;} // Navigation property
    }
}
