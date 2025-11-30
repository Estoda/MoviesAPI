namespace MoviesAPI.Services
{
    public interface IGenresService // Interface for genre-related operations
    {
        Task<IEnumerable<Genre>> GetAll();// Get all genres
        Task<Genre> Add(Genre genre); // Add a new genre
        Task<Genre?> GetById(int id); // Get a genre by its ID
        Genre Update(Genre genre); // Update an existing genre
        Genre Delete(Genre genre); // Delete a genre by its ID
    }
}
