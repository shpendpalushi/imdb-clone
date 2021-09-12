using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMDBClone.Domain.Definitions;

namespace IMDBClone.Domain.Service.Contracts
{
    public interface IMovieActorService
    {
        Task<Result> SaveActorsForMovieAsync(List<Guid> actorIds, Guid movieId);
    }
}