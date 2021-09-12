using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IMDBClone.Application.Controllers.Base;
using IMDBClone.Data.Seed;
using IMDBClone.Domain.Definitions;
using IMDBClone.Domain.DTO;
using IMDBClone.Domain.Extensions.Types;
using IMDBClone.Domain.Service.Contracts;
using IMDBClone.Domain.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBClone.Application.Controllers
{
    [Authorize(Roles="Admin,User")]
    [EnableCors("CorsApi")]
    public class MoviesController : BaseApiController
    {
        private readonly IMovieService _movieService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MoviesController(IMovieService movieService, IWebHostEnvironment webHostEnvironment)
        {
            _movieService = movieService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("api/movies")]
        public async Task<IActionResult> GetMovies(int page=1)
        {
            List<MovieDTO> movies = await _movieService.GetTopRatedMoviesForPageAsync(page);
            return Ok(movies);
        }

        [HttpGet("api/movie/{id}")]
        public async Task<IActionResult> GetMovie(Guid id)
        {
            MovieDTO movie = await _movieService.GetMovieByIdAsync(id);
            return Ok(movie);
        }

        [HttpPost("api/movie-add")]
        public async Task<IActionResult> PostMovie(MovieDTO movie)
        {
            MovieValidator validator = new MovieValidator();
            var validationResult = await validator.ValidateAsync(movie);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors.ToListString());
            Result result = await _movieService.CreateMovieAsync(movie);
            if (!result.Success) return BadRequest(result.Error);
            return Ok();
        }
        
        [HttpGet("api/movies-for-term")]
        public async Task<IActionResult> GetMoviesForSearchTerm(string searchTerm)
        {
            List<MovieDTO> movies = await _movieService.GetMoviesBySearchTerm(searchTerm);
            return Ok(movies);
        }

        [HttpGet("/api/picture/{id}")]
        public async Task<IActionResult> GetImageForMovie(Guid id)
        {
            MovieDTO movie = await _movieService.GetMovieByIdAsync(id);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "img", $"{movie.FileName}.png");
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, "image/png");
        }
        
        [HttpPost("/api/image-uploading/{id}")]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Index(Guid id, [FromForm] IFormFile file)
        {
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "img/" + file.FileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(file.FileName); 
        }

        
        
    }
}