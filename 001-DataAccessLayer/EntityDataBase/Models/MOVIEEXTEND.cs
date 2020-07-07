using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImdbServerCore
{
	public class MOVIEEXTEND
	{
		private string _movieImdbID;
		private string _moviePlot;
		private string _movieUrl;
		private string _movieRated;
		private float _movieImdbRating;
		private bool _movieSeen;
		private string _userID;

		private MOVy _MOVy;
		private USER _USER;

		[Key]
		[ForeignKey("MOVy")]
		public string movieImdbID {
			get { return _movieImdbID; }
			set { _movieImdbID = value; }
		}
		public MOVy MOVy {
			get { return _MOVy; }
			set { _MOVy = value; }
		}

		public string moviePlot {
			get { return _moviePlot; }
			set { _moviePlot = value; }
		}

		public string movieUrl {
			get { return _movieUrl; }
			set { _movieUrl = value; }
		}

		public string movieRated {
			get { return _movieRated; }
			set { _movieRated = value; }
		}

		public float movieImdbRating {
			get { return _movieImdbRating; }
			set { _movieImdbRating = value; }
		}

		public bool movieSeen {
			get { return _movieSeen; }
			set { _movieSeen = value; }
		}

		[ForeignKey("USER")]
		public string userID
		{
			get { return _userID; }
			set { _userID = value; }
		}
		public USER USER
		{
			get { return _USER; }
			set { _USER = value; }
		}

		public override string ToString()
		{
			return
				movieImdbID + " " +
				moviePlot + " " +
				movieUrl + " " +
				movieRated + " " +
				movieImdbRating + " " +
				movieSeen + " " +
				userID;
		}
	}
}
