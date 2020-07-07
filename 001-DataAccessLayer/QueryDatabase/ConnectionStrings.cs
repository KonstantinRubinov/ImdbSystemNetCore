using System.Collections.Generic;

namespace ImdbServerCore
{
	static public class ConnectionStrings
	{
		static private string connectionString = "Data Source =.; Initial Catalog = ImdbFavorites; Integrated Security = True";
		static private string mySqlConnectionString = "server=localhost; user id = root; persistsecurityinfo=True; password=Rk14101981; database=imdbfavorites";

		static public string ConnectionString = "mongodb://localhost:27017";
		static public string DatabaseName = "Imdb";
		static public string UsersCollectionName = "Users";
		static public string MoviesCollectionName = "Movies";

		

		static public string GetConnection()
		{
			return connectionString;
		}

		static public string GetMySqlConnection()
		{
			return mySqlConnectionString;
		}
	}
}
