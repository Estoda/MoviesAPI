using MoviesAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

public class GetAllMoviesQuiry
{

        private readonly IMoviesRepository _moviesRepository;
        private readonly IMapper _mapper;
        public GetAllMoviesQuiry(IMoviesRepository moviesRepository, IMapper mapper)
        {
            _moviesRepository = moviesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDetailsDto>> Handle()
        {
            var movies = (await _moviesRepository.GetAll()).ToList();

            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return data;
        }

}