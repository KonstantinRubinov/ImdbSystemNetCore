using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ImdbServerCore
{
	[Route("api")]
	[ApiController]
	[Authorize]
	public class MoviesApiController : ControllerBase
    {
		private IMoviesExtendRepository moviesExtendRepository;

		public MoviesApiController(IMoviesExtendRepository _moviesExtendRepository)
		{
			moviesExtendRepository = _moviesExtendRepository;
		}

		[HttpGet("movies")]
		public IActionResult GetAllMovies()
		{
			try
			{
				var userID = User.Identity.Name;
				List<MovieExtendModel> allMovies = moviesExtendRepository.GetAllMovies(userID);
				return Ok(allMovies);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("movies/favoriteWord/{byWord}")]
		public IActionResult GetByWord(string byWord)
		{
			try
			{
				var userID = User.Identity.Name;
				List<MovieExtendModel> movies = moviesExtendRepository.GetByWord(byWord, userID);
				return Ok(movies);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("movies/favoriteId/{byId}")]
		public IActionResult GetById(string byId)
		{
			try
			{
				var userID = User.Identity.Name;
				MovieExtendModel oneMovie = moviesExtendRepository.GetById(byId, userID);
				return Ok(oneMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("movies/favoriteTitle/{movieTitle}")]
		public IActionResult GetByTitle(string movieTitle)
		{
			try
			{
				var userID = User.Identity.Name;
				MovieExtendModel oneMovie = moviesExtendRepository.GetByTitle(movieTitle, userID);
				return Ok(oneMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpPost("movies")]
		public IActionResult AddMovie(MovieExtendModel movieModel)
		{
			try
			{
				if (movieModel == null)
				{
					return BadRequest("Data is null.");
				}
				//if (!ModelState.IsValid)
				//{
				//	Errors errors = ErrorsHelper.GetErrors(ModelState);
				//	return BadRequest(errors);
				//}
				var id = User.Identity.Name;
				movieModel.userID = id;
				MovieExtendModel addedMovie = moviesExtendRepository.AddMovie(movieModel);
				return StatusCode(StatusCodes.Status201Created, addedMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpPut("movies/{id}")]
		public IActionResult UpdateMovie(string id, MovieExtendModel movieModel)
		{
			try
			{
				if (movieModel == null)
				{
					return BadRequest("Data is null.");
				}
				//if (!ModelState.IsValid)
				//{
				//	Errors errors = ErrorsHelper.GetErrors(ModelState);
				//	return BadRequest(errors);
				//}
				var uid = User.Identity.Name;
				movieModel.userID = uid;
				movieModel.imdbID = id;
				MovieExtendModel updatedMovie = moviesExtendRepository.UpdateMovie(movieModel);
				return Ok(updatedMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpDelete("movies/{id}")]
		public IActionResult DeleteMovie(string id)
		{
			try
			{
				var userID = User.Identity.Name;
				int i = moviesExtendRepository.DeleteMovie(id, userID);
				return NoContent();
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}
	}
}