using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ImdbServerCore
{
	public class MOVy
	{
		private string _movieImdbID;
		private string _movieTitle;
		private string _moviePoster;
		private string _userID;
		private int _movieYear;

		private USER _USER;

		[Key]
		public string movieImdbID
		{
			get { return _movieImdbID; }
			set { _movieImdbID = value; }
		}

		public string movieTitle
		{
			get { return _movieTitle; }
			set { _movieTitle = value; }
		}

		public string moviePoster
		{
			get { return _moviePoster; }
			set { _moviePoster = value; }
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

		public int movieYear
		{
			get { return _movieYear; }
			set { _movieYear = value; }
		}

		public override string ToString()
		{
			return
				movieImdbID + " " +
				movieTitle + " " +
				moviePoster + " " +
				movieYear + " " +
				userID;
		}

		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public MOVy()
		{
			this.MOVIEEXTENDS = new HashSet<MOVIEEXTEND>();
		}

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<MOVIEEXTEND> MOVIEEXTENDS { get; set; }
	}
}
