using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Services;
using AutoMapper;

public class GetByIdQuery
{
    private readonly IMoviesRepository _moviesRepository;
    private readonly IMapper _mapper;

    public GetByIdQuery(IMoviesRepository moviesRepository, IMapper mapper)
    {
        _moviesRepository = moviesRepository;
        _mapper = mapper;
    }

    public async Task<MovieDetailsDto> Execute(int movieId = 0)
    {
        var movies = await _moviesRepository.GetById(movieId);

        var data = _mapper.Map<MovieDetailsDto>(movies);

        return data;
    }
}