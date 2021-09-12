using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IMDBClone.Application.Controllers.Base;
using IMDBClone.Domain.Definitions;
using IMDBClone.Domain.DTO;
using IMDBClone.Domain.DTO.User;
using IMDBClone.Domain.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IMDBClone.Application.Controllers
{
    [Authorize(Roles="Admin,User")]
    [EnableCors("CorsApi")]
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;
        private readonly IUserService _userService;

        public RatingController(IRatingService ratingService, IUserService userService)
        {
            _ratingService = ratingService;
            _userService = userService;
        }
        [HttpPost("/api/rating")]
        public async Task<IActionResult> CreateRate(RatingDTO rating)
        {
            var userDetails = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userDetails == null) return Unauthorized();
            Guid userId = Guid.Parse(userDetails);
            ApplicationUserDTO userDto = await _userService.GetUserByIdAsync(userId);
            rating.UserId = userDto.Id;
            Result result = await _ratingService.AddRatingAsync(rating);
            if (!result.Success) return BadRequest(result.Error);
            return Ok();

        } 
    }
}