using Microsoft.EntityFrameworkCore;
using WorkingWithEFCore.Models;
using static System.Console;

namespace WorkingWithEFCore;

public class Northwind {
	
	//Props map to the tables in the database
	public DbSet<Category>? Categories { get; set; }
	public DbSet<Product>? Products { get; set; }

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

	protected void OnModelCreating(ModelBuilder modelBuilder) {
	
		// Fluent API instead of attributes to limit length of cat name

		modelBuilder.Entity<Category>()
		            .Property(category => category.CategoryName )
		            .IsRequired() // Not null
		            .HasMaxLength( 15 );
		
		//Used to fix SQLites lack of decimal support
		if ( ProjectConstants.DbProvider == "SQLite" ) {
			modelBuilder.Entity<Product>()
			            .Property( product => product.Cost )
			            .HasConversion<double>();
		}

	}
}