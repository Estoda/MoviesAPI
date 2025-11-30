namespace MoviesAPI.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<Movie> Add(Movie movie);
        Task<Movie?> GetById(int id);
        Task<IEnumerable<Movie>> GetByGenreId(int genreId);
        Task<bool> IsValidGenre(int genreId);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);
    }
}
