using System;

namespace ImdbServerCore
{
	public class EntityBaseManager
	{
		public readonly ImdbFavoritesEntities imdbFavoritesEntities;

		public EntityBaseManager(ImdbFavoritesEntities context)
		{
			imdbFavoritesEntities = context;
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					imdbFavoritesEntities.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
