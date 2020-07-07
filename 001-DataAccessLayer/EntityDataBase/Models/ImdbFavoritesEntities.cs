using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ImdbServerCore
{
	public class ImdbFavoritesEntities : DbContext
	{
		public ImdbFavoritesEntities(DbContextOptions options) : base(options)
		{
		}

		public DbSet<MOVIEEXTEND> MOVIEEXTENDS { get; set; }
		public DbSet<MOVy> MOVIES { get; set; }
		public DbSet<USER> USERS { get; set; }

		public override int SaveChanges()
		{
			var entities = (from entry in ChangeTracker.Entries()
							where entry.State == EntityState.Modified || entry.State == EntityState.Added
							select entry.Entity);

			var validationResults = new List<ValidationResult>();
			foreach (var entity in entities)
			{
				if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
				{
					throw new ValidationException();
				}
			}
			return base.SaveChanges();
		}
	}
}

//Add-Migration ImdbServerCore.ImdbFavoritesEntities
//update-database
