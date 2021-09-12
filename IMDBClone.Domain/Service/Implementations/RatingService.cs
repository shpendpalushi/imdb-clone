using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IMDBClone.Data.Entities;
using IMDBClone.Domain.DTO;
using IMDBClone.Domain.Service.Contracts;

namespace IMDBClone.Domain.Service.Implementations
{
    public class RatingService : IRatingService
    {
        private readonly DataService _dataService;
        private readonly IMapper _mapper;

        public RatingService(DataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        public async Task<List<RatingDTO>> GetAllRatingsAsync()
        {
            return _mapper.Map<List<RatingDTO>>(await _dataService.GetAllAsNoTrackingAsync<Rating>());
        }

        public async Task<List<RatingDTO>> GetAllRatingsForMovie(Guid movieId)
        {
            List<Rating> ratingDtos = await _dataService.GetAllAsNoTrackingAsync<Rating>(r => r.MovieId == movieId);
            return _mapper.Map<List<RatingDTO>>(ratingDtos);
        }
    }
}