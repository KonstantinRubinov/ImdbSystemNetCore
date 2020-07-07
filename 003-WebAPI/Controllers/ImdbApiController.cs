using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbServerCore
{
	[Route("api")]
	[ApiController]
	[Authorize]
	public class ImdbApiController : ControllerBase
    {
		private IImdbRepository imdbRepository;
		private IUsersRepository userRepository;

		public ImdbApiController(IImdbRepository _imdbRepository, IUsersRepository _userRepository)
		{
			imdbRepository = _imdbRepository;
			userRepository = _userRepository;
		}


		[HttpGet("movies/imdbId/{movieId}")]
		public async Task<IActionResult> GetImdbById(string movieId)
		{
			//http://localhost:49270/api/imdbMovies/byId/tt3896198/

			var id = User.Identity.Name;

			try
			{
				UserModel userModel = userRepository.GetOneUserById(id);
				MovieExtendModel oneMovie = await imdbRepository.GetImdbById(userModel.userImdbPass, movieId, id);
				return Ok(oneMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("movies/imdbWord/{movieWord}")]
		public async Task<IActionResult> GetImdbByWord(string movieWord)
		{
			//http://localhost:49270/api/imdbMovies/byWord/matrix/

			var id = User.Identity.Name;

			try
			{
				UserModel userModel = userRepository.GetOneUserById(id);
				List<MovieModel> movies = await imdbRepository.GetImdbByWord(userModel.userImdbPass, movieWord, id);
				return Ok(movies);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("movies/imdbTitle/{movieTitle}")]
		public async Task<IActionResult> GetImdbByTitle(string movieTitle)
		{
			//http://localhost:49270/api/imdbMovies/byTitle/matrix/


			var id = User.Identity.Name;

			try
			{
				UserModel userModel = userRepository.GetOneUserById(id);
				MovieExtendModel oneMovie = await imdbRepository.GetImdbByTitle(userModel.userImdbPass, movieTitle, id);
				return Ok(oneMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}
	}
}