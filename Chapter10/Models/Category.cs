using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingWithEFCore.Models;

public class Category {
	public Category() {
		//Initialise navigation property to empty collection

		Products = new HashSet<Product>();
	}

	//Properties match to columns in the database
	public int    CategoryId   { get; set; }
	public string CategoryName { get; set; }

	[Column( TypeName = "ntext" )]
	public string Description { get; set; }

	// Navigation property
	public virtual ICollection<Product> Products { get; set; }
}