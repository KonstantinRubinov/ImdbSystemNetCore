namespace ImdbServerCore
{
	public interface IImdbDatabaseSettings
	{
		string UsersCollectionName { get; set; }
		string MoviesCollectionName { get; set; }
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }
	}
}
