using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace ImdbServerCore
{
	public class EntityMoviesManager : EntityBaseManager, IMoviesRepository
	{
		public EntityMoviesManager(ImdbFavoritesEntities context) : base(context)
		{
		}
		public List<MovieModel> GetAllMovies(string userID)
		{
			var resultQuary = imdbFavoritesEntities.MOVIES.Where(m => m.userID.Equals(userID)).Select(m => new MovieModel
			{
				userID = m.userID,
				imdbID = m.movieImdbID,
				title = m.movieTitle,
				poster = m.moviePoster,
				year = m.movieYear
			});

			//var resultSP = imdbFavoritesEntities.GetAllMovies(userID).Select(m => new MovieModel
			//{
			//	userId = m.userID,
			//	imdbID = m.movieImdbID,
			//	title = m.movieTitle,
			//	poster = m.moviePoster,
			//	year = m.movieYear
			//});

			//if (GlobalVariable.queryType == 0)
				return resultQuary.ToList();
			//else
			//	return resultSP.ToList();
		}

		public List<MovieModel> GetByWord(string word, string userID)
		{
			var resultQuary = imdbFavoritesEntities.MOVIES.Where(m => m.movieTitle.Contains(word) && m.userID.Equals(userID)).Select(m => new MovieModel
			{
				userID = m.userID,
				imdbID = m.movieImdbID,
				title = m.movieTitle,
				poster = m.moviePoster,
				year = m.movieYear
			});

			//var resultSP = imdbFavoritesEntities.GetByWord(word, userID).Select(m => new MovieModel
			//{
			//	userId = m.userID,
			//	imdbID = m.movieImdbID,
			//	title = m.movieTitle,
			//	poster = m.moviePoster,
			//	year = m.movieYear
			//});

			//if (GlobalVariable.queryType == 0)
				return resultQuary.ToList();
			//else
			//	return resultSP.ToList();
		}



		public MovieModel GetById(string imdbID, string userID)
		{
			var resultQuary = imdbFavoritesEntities.MOVIES.Where(m => m.movieImdbID.Equals(imdbID) && m.userID.Equals(userID)).Select(m => new MovieModel
			{
				userID = m.userID,
				imdbID = m.movieImdbID,
				title = m.movieTitle,
				poster = m.moviePoster,
				year = m.movieYear
			});

			//var resultSP = imdbFavoritesEntities.GetById(imdbID, userID).Select(m => new MovieModel
			//{
			//	userId = m.userID,
			//	imdbID = m.movieImdbID,
			//	title = m.movieTitle,
			//	poster = m.moviePoster,
			//	year = m.movieYear
			//});

			//if (GlobalVariable.queryType == 0)
				return resultQuary.SingleOrDefault();
			//else
			//	return resultSP.SingleOrDefault();
		}

		public MovieModel GetByTitle(string title, string userID)
		{
			var resultQuary = imdbFavoritesEntities.MOVIES.Where(m => m.movieTitle.Equals(title) && m.userID.Equals(userID)).Select(m => new MovieModel
			{
				userID = m.userID,
				imdbID = m.movieImdbID,
				title = m.movieTitle,
				poster = m.moviePoster,
				year = m.movieYear
			});

			//var resultSP = imdbFavoritesEntities.GetByTitle(title, userID).Select(m => new MovieModel
			//{
			//	userId = m.userID,
			//	imdbID = m.movieImdbID,
			//	title = m.movieTitle,
			//	poster = m.moviePoster,
			//	year = m.movieYear
			//});

			//if (GlobalVariable.queryType == 0)
				return resultQuary.SingleOrDefault();
			//else
			//	return resultSP.SingleOrDefault();
		}

		public MovieModel AddMovie(MovieModel movieModel)
		{
			//var resultSP = imdbFavoritesEntities.AddMovie(movieModel.imdbID, movieModel.title, movieModel.poster, movieModel.year, movieModel.userId).Select(m => new MovieModel
			//{
			//	userId = m.userID,
			//	imdbID = m.movieImdbID,
			//	title = m.movieTitle,
			//	poster = m.moviePoster,
			//	year = m.movieYear
			//});

			//if (GlobalVariable.queryType == 0)
				return AddMovieQuery(movieModel);
			//else
			//	return resultSP.SingleOrDefault();
		}

		public MovieModel UpdateMovie(MovieModel movieModel)
		{
			//var resultSP = imdbFavoritesEntities.AddMovie(movieModel.imdbID, movieModel.title, movieModel.poster, movieModel.year, movieModel.userId).Select(m => new MovieModel
			//{
			//	userId = m.userID,
			//	imdbID = m.movieImdbID,
			//	title = m.movieTitle,
			//	poster = m.moviePoster,
			//	year = m.movieYear
			//});

			//if (GlobalVariable.queryType == 0)
				return UpdateMovieQuery(movieModel);
			//else
			//	return resultSP.SingleOrDefault();
		}

		public int DeleteMovie(string id, string userID)
		{
			//var resultSP = imdbFavoritesEntities.DeleteMovie(id, userID);

			//if (GlobalVariable.queryType == 0)
				return DeleteMovieQuery(id, userID);
			//else
			//	return resultSP;
		}

		private MovieModel AddMovieQuery(MovieModel movieModel)
		{
			MOVy movie = new MOVy
			{
				userID = movieModel.userID,
				movieImdbID = movieModel.imdbID,
				movieTitle = movieModel.title,
				moviePoster = movieModel.poster,
				movieYear = movieModel.year
			};
			imdbFavoritesEntities.MOVIES.Add(movie);
			try
			{
				imdbFavoritesEntities.SaveChanges();
			}
			catch (ValidationException ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
			return GetById(movie.movieImdbID, movie.userID);
		}


		private MovieModel UpdateMovieQuery(MovieModel movieModel)
		{
			MOVy movie = imdbFavoritesEntities.MOVIES.Where(m => m.movieImdbID.Equals(movieModel.imdbID)).SingleOrDefault();
			if (movie == null)
				return null;
			movie.userID = movieModel.userID;
			movie.movieImdbID = movieModel.imdbID;
			movie.movieTitle = movieModel.title;
			movie.moviePoster = movieModel.poster;
			movie.movieYear = movieModel.year;
			try
			{
				imdbFavoritesEntities.SaveChanges();
			}
			catch (ValidationException ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
			return GetById(movie.movieImdbID, movie.userID);
		}


		private int DeleteMovieQuery(string imdbID, string userID)
		{
			MOVy movie = imdbFavoritesEntities.MOVIES.Where(m => m.movieImdbID.Equals(imdbID) && m.userID.Equals(userID)).SingleOrDefault();
			imdbFavoritesEntities.MOVIES.Attach(movie);
			if (movie == null)
				return 0;
			imdbFavoritesEntities.MOVIES.Remove(movie);
			try
			{
				imdbFavoritesEntities.SaveChanges();
			}
			catch (ValidationException ex)
			{
				Debug.WriteLine(ex.Message);
				return 0;
			}
			return 1;
		}
	}
}
