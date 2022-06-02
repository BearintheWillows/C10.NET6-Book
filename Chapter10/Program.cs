using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage; //IDbContextTransaction
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorkingWithEFCore;
using WorkingWithEFCore.Logger;
using WorkingWithEFCore.Models;

Console.WriteLine( $"Using {ProjectConstants.DbProvider} database provider" );

//QueryingCategories();
// FilteredIncludes();
//QueryingProducts();
// QueryingWithLike();

/*string name = "Bob";
for ( int i = 0; i < 3; i++ )
{
    AddProduct( 1, name, 20M );
    name = name + 1;
}*/

//if ( AddProduct( categoryId: 6, productName: "Bob's Burgers", price: 500M ) )
//{
//  Console.WriteLine( "Add Product Successful" );
//}
//ListProducts();


/*if ( IncreasePrice( productNameStartsWith: "Bob", amount: 20M ) )
{
    Console.WriteLine( "Update product price Successful" );
}
ListProducts();
*/

int deleted = DeleteProducts(productNameStartsWith: "Bob");

Console.WriteLine( $"{deleted} product(s) were deleted." );


static void QueryingCategories()
{
    Northwind db = new();

    ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
    loggerFactory.AddProvider( new ConsoleLoggerProvider() );

    Console.WriteLine( "Categories and how many products they have:" );

    //Get all categories and the number of products in each

    IQueryable<Category>? categories;
    // = db.Categories;
    // .Include(c => c.Products);

    db.ChangeTracker.LazyLoadingEnabled = false;
    Console.Write( "Enable eager loading? (Y/N): " );
    bool eagerLoading = (Console.ReadKey().Key == ConsoleKey.Y);
    bool explicitLoading = false;
    Console.WriteLine();

    if ( eagerLoading )
    {
        categories = db.Categories?.Include( c => c.Products );
    }
    else
    {
        categories = db.Categories;
        Console.Write( "Enable Explicit loading? (Y/N): " );
        explicitLoading = ( Console.ReadKey().Key == ConsoleKey.Y );
        Console.WriteLine();
    }




    if ( categories != null ) //Execute query and enumerate the results
    {
        foreach ( var item in categories )
        {
            if ( explicitLoading )
            {
                Console.Write( $"Explicitly load products for {item.CategoryName}? (Y/N): " );
                ConsoleKeyInfo key = Console.ReadKey();

                if ( key.Key == ConsoleKey.Y )
                {
                    CollectionEntry<Category, Product> products =
                        db.Entry(item).Collection(c2 => c2.Products);
                    if ( !products.IsLoaded ) products.Load();
                }
            }

            Console.WriteLine( $"{item.CategoryName} has {item.Products?.Count} products." );
        }

    }
    else
    {
        Console.WriteLine( "No categories found" );
    }
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

static bool AddProduct( int categoryId, string productName, decimal? price )
{
    Northwind db = new();

    Product product = new()
    {
        CategoryID = categoryId,
        ProductName = productName,
        Cost = price,
    };

    //Mark product as added
    db.Products.Add( product );

    //Save tracked change

    int affected = db.SaveChanges();
    return ( affected == 1 );
}

static void ListProducts()
{
    Northwind db = new();

    Console.WriteLine( "{0, -3} {1, -35} {2,8} {3,5} {4}",
        "Id",
        "Product Name",
        "Cost",
        "Stock",
        "Disc" );

    foreach ( var item in db.Products.OrderByDescending( product
         => product.Cost ) )
    {
        Console.WriteLine( "{0:000} {1, -35} {2,8:$#,##0.00} {3,5} {4}",
            item.ProductID,
            item.ProductName,
            item.Cost,
            item.Stock,
            item.Discontinued );
    }
}

static bool IncreasePrice( string productNameStartsWith, decimal amount )
{
    Northwind db = new();

    //Get first product whos name starts with name
    var updateProduct = db.Products.First(p =>
            p.ProductName.StartsWith(productNameStartsWith));

    updateProduct.Cost += amount;

    int affected = db.SaveChanges();

    return ( affected == 1 );
}

static int DeleteProducts( string productNameStartsWith )
{
    Northwind db = new();

    using ( IDbContextTransaction t = db.Database.BeginTransaction() )
    {
        Console.WriteLine( "Transaction isolation level: {0}",
            arg0: t.GetDbTransaction().IsolationLevel );

        IQueryable<Product>? products = db.Products?.Where(p =>
    p.ProductName.StartsWith(productNameStartsWith));

        if ( products is null )
        {
            Console.WriteLine( "No Products found" );
            return 0;
        }
        else
        {
            db.Products.RemoveRange( products );
        }

        int affected = db.SaveChanges();
        t.Commit();
        return affected;
    }
}
