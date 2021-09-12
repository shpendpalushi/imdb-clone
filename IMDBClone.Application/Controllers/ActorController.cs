using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMDBClone.Application.Controllers.Base;
using IMDBClone.Domain.Definitions;
using IMDBClone.Domain.DTO;
using IMDBClone.Domain.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IMDBClone.Application.Controllers
{
    public class ActorController : BaseApiController
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet("api/actors/all")]
        public async Task<IActionResult> GetActors()
        {
            List<ActorDTO> actorDtos = await _actorService.GetActorsAsync();
            return Ok(actorDtos);
        }

        [HttpGet("api/actors/actor-by-id/{id}")]
        public async Task<IActionResult> GetActorById(Guid id)
        {
            ActorDTO actor = await _actorService.GetActorByIdAsync(id);
            return Ok(actor);
        }

        [HttpPost("api/actors/actor-save")]
        public async Task<IActionResult> SaveActor(ActorDTO actorDto)
        {
            Result result = await _actorService.SaveActorAsync((actorDto));
            if (!result.Success) return BadRequest(result.Error);
            return Ok();
        }
    }
}