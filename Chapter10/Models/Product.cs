namespace WorkingWithEFCore.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    public int ProductID { get; set; } // Primary Key

    [Required, StringLength( 40 )]
    public string? ProductName { get; set; } = null;

    [Column( "UnitPrice", TypeName = "money" )]
    public decimal? Cost { get; set; } //prop name != column name

    [Column( "UnitsInStock" )]
    public short? Stock { get; set; }

    public bool Discontinued { get; set; }

    //Define foreign key relationship to Category table

    public int CategoryID { get; set; }

    public virtual Category? Category { get; set; } = null;
}