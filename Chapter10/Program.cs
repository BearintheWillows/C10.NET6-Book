using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorkingWithEFCore;
using WorkingWithEFCore.Logger;
using WorkingWithEFCore.Models;

Console.WriteLine( $"Using {ProjectConstants.DbProvider} database provider" );

QueryingCategories();
// FilteredIncludes();
//QueryingProducts();
// QueryingWithLike();

static void QueryingCategories()
{
    Northwind db = new();

    ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
    loggerFactory.AddProvider( new ConsoleLoggerProvider() );

    Console.WriteLine( "Categories and how many products they have:" );

    //Get all categories and the number of products in each

    IQueryable<Category>? categories = db.Categories;
    //.Include( c => c.Products );

    if ( categories != null ) //Execute query and enumerate the results

        foreach ( var item in categories )
            Console.WriteLine( $"{item.CategoryName} has {item.Products?.Count} products." );
    else
        Console.WriteLine( "No categories found" );
}

static void FilteredIncludes()
{
    Northwind db = new();


    Console.WriteLine( "Enter a minimum for units in stock" );
    var stock = int.Parse(Console.ReadLine() ?? "10");

    var categories = db.Categories?
                       .Include(c => c.Products)
                       .Where(c => c.Products.Any(p => p.Stock >= stock));

    if ( categories != null )
    { //Execute query and enumerate the results

        Console.WriteLine( $"ToQueryString: {categories.ToQueryString()}" );

        foreach ( var category in categories )
        {
            Console.WriteLine();
            Console.WriteLine(
                $"{category.CategoryName} has {category.Products?.Count} products with a minimum of {stock} units in stock."
            );
            Console.WriteLine();
            foreach ( var item in category.Products )
                Console.WriteLine( $"{item.ProductName} has {item.Stock} units in stock." );
        }

    }
    else
    {
        Console.WriteLine( "No categories found" );
    }
}

static void QueryingProducts()
{
    Northwind db = new();

    ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
    loggerFactory.AddProvider( new ConsoleLoggerProvider() );

    Console.WriteLine( "Products that cost more than a given price:" ); //Highest at top

    string? input;
    decimal price;

    do
    {
        Console.Write( "Enter a price: " );
        input = Console.ReadLine();
    } while ( !decimal.TryParse( input, out price ) );

    IQueryable<Product>? products = db.Products?
                                      .Where(p => p.Cost > price)
                                      .OrderByDescending(p => p.Cost);

    if ( products is null )
        Console.WriteLine( "No products found" );
    else
        foreach ( var item in products )
            Console.WriteLine( "{0}: {1} costs {1:$#,##0.00} and has {2} units in stock.",
                               item.ProductName,
                               item.Cost,
                               item.Stock
            );
}

static void QueryingWithLike()
{
    Northwind db = new();

    ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
    loggerFactory.AddProvider( new ConsoleLoggerProvider() );

    Console.Write( "Enter part of a product name: " );

    string? input = Console.ReadLine();
    IQueryable<Product>? products = db.Products?
        .Where(p =>
        EF.Functions.Like(p.ProductName, $"%{input}%"));

    if ( products is null )
    {
        Console.WriteLine( "No Product found" );
        return;
    }
    foreach ( var p in products )
    {
        Console.WriteLine( "{0} has {1} units in stock. Discontinued? {2}",
            p.ProductName,
            p.Stock,
            p.Discontinued );
    }
}
