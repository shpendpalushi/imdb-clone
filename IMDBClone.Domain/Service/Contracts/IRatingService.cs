using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMDBClone.Data.Entities;
using IMDBClone.Domain.DTO;

namespace IMDBClone.Domain.Service.Contracts
{
    public interface IRatingService
    {
        Task<List<RatingDTO>> GetAllRatingsAsync();
        Task<List<RatingDTO>> GetAllRatingsForMovie(Guid movieId);
    }
}