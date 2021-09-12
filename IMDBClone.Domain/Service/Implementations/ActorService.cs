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
    public class ActorService : IActorService
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public ActorService(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        public async Task<List<ActorDTO>> GetActorsAsync()
        {
            List<Actor> actors = await _dataService.GetAllAsNoTrackingAsync<Actor>();
            return _mapper.Map<List<ActorDTO>>(actors);
        }

        public async Task<ActorDTO> GetActorByIdAsync(Guid id)
        {
            Actor actor = await _dataService.GetAsNoTrackingAsync<Actor>(id);
            return _mapper.Map<ActorDTO>(actor);
        }

        public async Task<Result> SaveActorAsync(ActorDTO actor)
        {
            Actor a = _mapper.Map<Actor>(actor);
            Result result = await _dataService.AddOrUpdateAsync(a);
            return result;
        }

        public async Task<ICollection<ActorDTO>> GetActorsFromIdListAsync(List<Guid> dataList)
        {
            List<Actor> actors = new ();
            foreach (var id in dataList)
            {
                actors.Add(await _dataService.GetAsync<Actor>(id));
            }

            return _mapper.Map<List<ActorDTO>>(actors);
        }

        public async Task<List<ActorDTO>> GetActorListFromIdListAsync(List<Guid> id)
        {
            List<Actor> actors = new();
            foreach (var el in id)
            {
                Actor a = await _dataService.GetAsNoTrackingAsync<Actor>(el);
                actors.Add(a);
            }

            return _mapper.Map<List<ActorDTO>>(actors);
        }
    }
}