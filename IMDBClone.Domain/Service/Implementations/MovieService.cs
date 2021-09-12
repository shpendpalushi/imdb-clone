using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using IMDBClone.Data.Commons.Enums;
using IMDBClone.Data.Entities;
using IMDBClone.Domain.Definitions;
using IMDBClone.Domain.DTO;
using IMDBClone.Domain.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IMDBClone.Domain.Service.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public MovieService(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        public async Task<List<MovieDTO>> GetTopRatedMoviesForPageAsync(int page)
        {
            List<Movie> movies = await _dataService.GetAllAsNoTrackingAsync(includeExpression: m => m.Include(m => m.Ratings).Include(m=> m.Cast).ThenInclude(c => c.Actor), 
                orderExpression: new List<Expression<Func<Movie, object>>> {
                m =>  m.Ratings.Sum(r => r.Rate) / m.Ratings.Count()
             }, byDescending: true, take: 10, skip: (page - 1) * 10);
            List<MovieDTO> m =  _mapper.Map<List<MovieDTO>>(movies);
            foreach (var el in m)
            {
                el.AverageRating = el.Ratings.Count == 0 ? 0 : (double) el.Ratings.Sum(r => r.Rate) / el.Ratings.Count;
            }
            return m;
        }

        public async Task<List<MovieDTO>> GetMoviesBySearchTerm(string searchTerm)
        {
            List<Movie> allMovies = await _dataService.GetAllAsNoTrackingAsync<Movie>();
            List<Movie> movies = await _dataService.GetAllAsNoTrackingAsync(
                includeExpression: m => m.Include(mo => mo.Ratings).Include(m=> m.Cast).ThenInclude(c => c.Actor),
                whereExpression: m => m.Title.Contains(searchTerm) || 
                                      m.Description.Contains(searchTerm), orderExpression: new List<Expression<Func<Movie, object>>> {
                m => m.Ratings.Sum(r => r.Rate)
            });

            List<Movie> matchingMovies = GetMoviesFOrSearchTerm(allMovies, searchTerm);
            List<Movie> resultMovies = movies.Union(matchingMovies).ToList();
            List<MovieDTO> m = _mapper.Map<List<MovieDTO>>(resultMovies);
            foreach (var el in m)
            {
                el.AverageRating = el.Ratings.Count == 0 ? 0 : (double) el.Ratings.Sum(r => r.Rate) / el.Ratings.Count;
            }
            return m.OrderBy(m => m.AverageRating).ToList();
        }
        
        public async Task<MovieDTO> GetMovieByIdAsync(Guid movieId)
        {
            return _mapper.Map<MovieDTO>(await _dataService.GetAsNoTrackingAsync<Movie>(movieId));
        }

        public async Task<Result<MovieDTO>> SaveMovieAsync(MovieDTO movie)
        {
            DateTime date = DateTime.Now;
            Movie m = _mapper.Map<Movie>(movie);
            m.CreatedAt = date;
            m.ModifiedAt = DateTime.Now;
            Result result = await _dataService.AddOrUpdateAsync(m);
            return result.Success ? Result.Ok<MovieDTO>(_mapper.Map<MovieDTO>(m)) : Result.Fail<MovieDTO>(error: result.Error);
        }
        
        public async Task<List<MovieDTO>> GetMoviesByTypeAsync(MovieTypeEnum type, int page)
        {
            List<Movie> movies = await _dataService.GetAllAsNoTrackingAsync<Movie>(
                includeExpression: m => m.Include(m => m.Ratings).Include(m => m.Cast).ThenInclude(c => c.Actor),
                whereExpression: m => m.MovieType == type, byDescending: true, take: 10, skip: (page - 1) * 10);
            List<MovieDTO> m =  _mapper.Map<List<MovieDTO>>(movies);
            foreach (var el in m)
            {
                el.AverageRating = el.Ratings.Count == 0 ? 0 : (double) el.Ratings.Sum(r => r.Rate) / el.Ratings.Count;
            }
            return m;
        }

        #region Helpers

        private List<Movie> GetMoviesFOrSearchTerm(List<Movie> movies, string searchTerm)
        {
            List<Movie> moviesToReturn = movies;
            int val;
            bool existsInt = int.TryParse(Regex.Match(searchTerm, @"^\d+").ToString(), out val);
            if (existsInt)
            {
                if (searchTerm.Contains("star"))
                {
                    if (searchTerm.Contains("less"))
                    {
                        moviesToReturn = moviesToReturn.Where(m =>
                            (m.Ratings.Count > 0 && (m.Ratings.Sum(r => r.Rate) / m.Ratings.Count) < val) ||
                            m.Ratings.Count == 0).ToList();
                    }

                    if (searchTerm.Contains("more") || searchTerm.Contains("least"))
                    {
                        moviesToReturn = moviesToReturn.Where(m =>
                            (m.Ratings.Count > 0 && (m.Ratings.Sum(r => r.Rate) / m.Ratings.Count) > val)).ToList();
                    }
                }
            }
            else  if (searchTerm.Contains("after"))
            {
                moviesToReturn = moviesToReturn.Where(m => m.ReleaseDate.Year > val).ToList();
            }
            else if (searchTerm.Contains("before"))
            {
                moviesToReturn = moviesToReturn.Where(m => m.ReleaseDate.Year < val).ToList();
            }
            else if (searchTerm.Contains(("old")) && searchTerm.Contains("year"))
            {
                moviesToReturn = moviesToReturn.Where(m => (DateTime.Now.Year - m.ReleaseDate.Year) > val).ToList();
            }
            else if (searchTerm.Contains(("new")) && searchTerm.Contains("year"))
            {
                moviesToReturn = moviesToReturn.Where(m => (DateTime.Now.Year - m.ReleaseDate.Year) < val).ToList();
            }

            return moviesToReturn;


        }
        #endregion
    }
}