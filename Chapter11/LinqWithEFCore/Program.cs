using LinqWithEFCore;
using Microsoft.EntityFrameworkCore; //DbSet<T>
using static System.Console;

//FilterAndSort();
//JoinCategoriesAndProducts();
GroupJoinCategoriesAndProducts();

static void FilterAndSort()
{
    Northwind db = new();

    DbSet<Product> allProducts = db.Products;

    IQueryable<Product> filteredProducts = allProducts
        .Where(p => p.UnitPrice < 10M);

    IOrderedQueryable<Product> sortedAndFilteredProducts = filteredProducts
        .OrderByDescending(p => p.UnitPrice);

    var projectedProducts = sortedAndFilteredProducts
        .Select(product => new //anon type
        {
            product.ProductId,
            product.ProductName,
            product.UnitPrice,
        });

    WriteLine( "Products that cost less than $10:" );

    foreach ( var item in projectedProducts )
    {
        WriteLine( $" {item.ProductId}: {item.ProductName} costs {item.UnitPrice: $#,##0.00}" );
    }
    WriteLine();
}


//Joining and Group Join

static void JoinCategoriesAndProducts()
{
    Northwind db = new();

    //Join every products to its category to return 77 matches

    var queryJoin = db.Categories.Join(
        inner: db.Products,
        outerKeySelector: c => c.CategoryId,
        innerKeySelector: p => p.CategoryId,
        resultSelector: (c, p) => new
        {
            CategoryName = c.CategoryName,
            ProductName = p.ProductName,
            ProductId = p.ProductId
        })
        .OrderBy(cp => cp.CategoryName);

    foreach ( var item in queryJoin )
    {
        WriteLine( "{0}: {1} is in {2}.",
            item.ProductId, item.ProductName, item.CategoryName );
    }
}

// Group Join

static void GroupJoinCategoriesAndProducts()
{
    Northwind db =new();

    //Group all products by their category

    var queryGroup = db.Categories.AsEnumerable().GroupJoin(
        inner: db.Products,
        outerKeySelector: c => c.CategoryId,
        innerKeySelector: p => p.CategoryId,
        resultSelector: (c, matchingProducts) => new
        {
            CategoryName = c.CategoryName,
            Products = matchingProducts.OrderBy(p => p.ProductName)
        });

    foreach ( var item in queryGroup )
    {
        WriteLine( "{0} has {1} products.",
            item.CategoryName, item.Products.Count() );
        foreach ( var product in item.Products )
        {
            WriteLine( $" {product.ProductName}" );
        }
    }
}