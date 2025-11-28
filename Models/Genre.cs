namespace MoviesAPI.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }
    }
}
