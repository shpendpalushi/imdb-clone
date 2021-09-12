using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IMDBClone.Application.Controllers.Base;
using IMDBClone.Data.Commons.Enums;
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
        private readonly IActorService _actorService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMovieActorService _movieActorService;

        public MoviesController(IMovieService movieService, IActorService actorService, 
            IWebHostEnvironment webHostEnvironment, IMovieActorService movieActorService)
        {
            _movieService = movieService;
            _actorService = actorService;
            _webHostEnvironment = webHostEnvironment;
            _movieActorService = movieActorService;
        }
        [HttpGet("api/movies")]
        public async Task<IActionResult> GetMovies(int page=1)
        {
            List<MovieDTO> movies = await _movieService.GetTopRatedMoviesForPageAsync(page);
            List<MovieDTO> dataMovies = await AddReferralData(movies);
            return Ok(dataMovies);
        }

        [HttpGet("api/movie/{id}")]
        public async Task<IActionResult> GetMovie(Guid id)
        {
            MovieDTO movie = await _movieService.GetMovieByIdAsync(id);
            return Ok(movie);
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

        [HttpPost("/api/post/movie")]
        public async Task<IActionResult> Post(IFormCollection formdata)
        {
            var files = HttpContext.Request.Form.Files;
            var imageFile = formdata["imageFile"];
            string fileName = "";
            foreach (var file in files)
            {
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                if (file.Length > 0)
                {
                    fileName = Guid.NewGuid().ToString() + file.FileName; // Give file name
                    await using var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
                    await file.CopyToAsync(fileStream);
                }
            }
            var title = formdata["title"][0];
            var releaseDate = formdata["releaseDate"][0];
            var selectActors = formdata["selectActors"][0].Trim().Split(",");
            var description = formdata["description"][0];
            var movieType = formdata["movieType"][0];
            List<Guid> actorIdList = new ();
            foreach (var elem in selectActors)
            {
                Guid myGuid;
                bool isValid = Guid.TryParse(elem, out myGuid);
                if(isValid)
                    actorIdList.Add(myGuid);
            }
            DateTime date = DateTime.ParseExact(releaseDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            ICollection<ActorDTO> actorList = await _actorService.GetActorsFromIdListAsync(actorIdList);
            int movieTypeEnum = int.Parse(movieType);
            MovieDTO movieDto = new()
            {
                Title = title,
                Description = description,
                ReleaseDate = date,
                FileName = fileName,
                MovieType = (MovieTypeEnum) movieTypeEnum,
                Actors = actorList
            };
            MovieValidator validator = new MovieValidator();
            var validationResult = await validator.ValidateAsync(movieDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors.ToListString());
            
            Result<MovieDTO> result = await _movieService.SaveMovieAsync(movieDto);
            if (!result.Success) return BadRequest(result.Error);
            Result mResult = await _movieActorService.SaveActorsForMovieAsync(actorIdList, result.Value.MovieId);
            if (!mResult.Success) return BadRequest(mResult.Error);
            return Ok();
        }

        [NonAction]
        private async Task<List<MovieDTO>> AddReferralData(List<MovieDTO> movies)
        {
            foreach (var m in movies)
            {
                var ids = m.Cast.Select(c => c.ActorId).ToList();
                m.Actors = await _actorService.GetActorsFromIdListAsync(ids);
            }

            return movies;
        } 

        
        
    }
}