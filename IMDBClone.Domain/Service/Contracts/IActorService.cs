using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMDBClone.Domain.Definitions;
using IMDBClone.Domain.DTO;

namespace IMDBClone.Domain.Service.Contracts
{
    public interface IActorService
    {
        Task<List<ActorDTO>> GetActorsAsync();
        Task<ActorDTO> GetActorByIdAsync(Guid id);
        Task<Result> SaveActorAsync(ActorDTO actor);
        Task<ICollection<ActorDTO>> GetActorsFromIdListAsync(List<Guid> dataList);
        Task<List<ActorDTO>> GetActorListFromIdListAsync(List<Guid> id);
    }
}