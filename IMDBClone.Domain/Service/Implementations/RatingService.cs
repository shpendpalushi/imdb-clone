using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IMDBClone.Data.Entities;
using IMDBClone.Domain.Definitions;
using IMDBClone.Domain.DTO;
using IMDBClone.Domain.Service.Contracts;

namespace IMDBClone.Domain.Service.Implementations
{
    public class RatingService : IRatingService
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public RatingService(IDataService dataService, IMapper mapper)
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

        public async Task<Result> AddRatingAsync(RatingDTO rating)
        {
            Rating r = await _dataService.FirstOrDefaultAsNoTrackingAsync<Rating>(whereExpression: r =>
                r.UserId == rating.UserId && r.MovieId == rating.MovieId);
            if(r != null)
                return Result.Fail(error: "You already have rated this movie");
            Rating rate = _mapper.Map<Rating>(rating);
            return await _dataService.AddOrUpdateAsync(rate);
        }
    }
}