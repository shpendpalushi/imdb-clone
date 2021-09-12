using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IMDBClone.Data.Entities;
using IMDBClone.Domain.Definitions;
using IMDBClone.Domain.Service.Contracts;

namespace IMDBClone.Domain.Service.Implementations
{
    public class MovieActorService : IMovieActorService
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public MovieActorService(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        public async Task<Result> SaveActorsForMovieAsync(List<Guid> actorIds, Guid movieId)
        {
            DateTime date = DateTime.Now;
            List<MovieActor> dataList = new List<MovieActor>();
            foreach (var actorId in actorIds)
            {
                MovieActor movieActor = new()
                {
                    MovieId = movieId,
                    ActorId = actorId,
                    CreatedAt = date,
                    ModifiedAt = date,
                };
                dataList.Add(movieActor);
            }

            Result result = await _dataService.AddRangeAsync(dataList);
            return result;
        }
    }
}