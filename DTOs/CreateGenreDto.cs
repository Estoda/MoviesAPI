namespace MoviesAPI.DTOs
{
    public class CreateGenreDto
    {
        [MaxLength(100)]
        public required string Name { get; set; }
    }
}
