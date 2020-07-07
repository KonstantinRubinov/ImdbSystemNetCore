namespace ImdbServerCore
{
	public class ImdbDatabaseSettings: IImdbDatabaseSettings
	{
		public string UsersCollectionName { get; set; }
		public string MoviesCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}
}
