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
            List<Movie> movies = await _dataService.GetAllAsNoTrackingAsync(includeExpression: m => m.Include(m => m.Ratings).Include(m=> m.Cast).ThenInclude(c => c.Actor), orderExpression: new List<Expression<Func<Movie, object>>> {
                m => m.Ratings.Count > 0 ?  m.Ratings.Sum(r => r.Rate) / m.Ratings.Count() : 0
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
            List<Movie> movies = await _dataService.GetAllAsNoTrackingAsync(includeExpression: m => m.Include(mo => mo.Ratings).Include(m=> m.Cast).ThenInclude(c => c.Actor), whereExpression: m => m.Title.Contains(searchTerm) || m.Description.Contains(searchTerm) || GetByTerm(searchTerm, m), orderExpression: new List<Expression<Func<Movie, object>>> {
                m => m.Ratings.Sum(r => r.Rate) / m.Ratings.Count()
            }, byDescending: true);
            List<MovieDTO> m = _mapper.Map<List<MovieDTO>>(movies);
            foreach (var el in m)
            {
                el.AverageRating = el.Ratings.Count == 0 ? 0 : (double) el.Ratings.Sum(r => r.Rate) / el.Ratings.Count;
            }
            return m;
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
        private bool GetByTerm(string searchTerm, Movie m)
        {
            double starAverage = 0;
            if (m.Ratings.Count > 0)
            {
                starAverage = ((double)m.Ratings.Sum(r => r.Rate) /m.Ratings.Count);
            }
            int val;
            bool existsInt = int.TryParse(Regex.Match(searchTerm, @"^\d+").ToString(), out val);
            if (existsInt)
            {
                if (searchTerm.Contains("stars"))
                {
                    if (searchTerm.Contains("more"))
                    {
                        if (starAverage > val)
                            return true;
                    }
                    else
                    {
                        if (searchTerm.Contains("less"))
                        {
                            if (starAverage < val)
                                return true;
                        }
                        else
                        {
                            if ((int) starAverage == val)
                                return true;
                        }
                    }
                }
                else
                {
                    if (searchTerm.Contains("old"))
                    {
                        if (searchTerm.Contains("years"))
                        {
                            if (DateTime.Now.Year - m.ReleaseDate.Year > val)
                                return true;
                        }
                    }
                    else if (searchTerm.Contains("after"))
                    {
                        if (m.ReleaseDate.Year > val)
                            return true;
                    }
                    else if (searchTerm.Contains("before"))
                    {
                        if (m.ReleaseDate.Year < val)
                            return true;
                    }
                    else
                    {
                        if (m.ReleaseDate.Year == val)
                            return true;
                    }
                }
            }

            return false;
        }
        #endregion
    }
}