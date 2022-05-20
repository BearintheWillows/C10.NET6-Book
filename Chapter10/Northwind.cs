using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace WorkingWithEFCore;

public class Northwind {
	protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		if ( ProjectConstants.DbProvider == "SQLite" ) {
			var path = Path.Combine( Environment.CurrentDirectory, "Northwind.db" );
			WriteLine();
			WriteLine( $"Using {path} database file" );

			optionsBuilder.UseSqlite( $"Filename={path}" );
		} else {
			var connection = "Data Source=.;" +
			                 "Initial Catalog=Northwind;" +
			                 "Integrated Security=true;" +
			                 "MultipleActiveResultSets=true;";
			optionsBuilder.UseSqlServer( connection );
		}
	}
}