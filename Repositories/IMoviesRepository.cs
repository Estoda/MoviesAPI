namespace MoviesAPI.Services
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> GetAll(int movieId = 0);
        Task<Movie> Add(Movie movie);
        Task<IEnumerable<Movie>> GetByGenreId(int genreId);
        Task<Movie?> GetById(int movieId);
        Task<bool> IsValidGenre(int genreId);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);
    }
}
