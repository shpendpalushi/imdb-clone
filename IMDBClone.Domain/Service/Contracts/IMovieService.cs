using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMDBClone.Domain.Definitions;
using IMDBClone.Domain.DTO;

namespace IMDBClone.Domain.Service.Contracts
{
    public interface IMovieService
    {
        Task<List<MovieDTO>> GetTopRatedMoviesForPageAsync(int page);
        Task<List<MovieDTO>> GetMoviesBySearchTerm(string searchTerm);
        Task<MovieDTO> GetMovieByIdAsync(Guid movieId);
        Task<Result> CreateMovieAsync(MovieDTO movie);
    }
}