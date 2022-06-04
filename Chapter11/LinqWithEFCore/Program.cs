using LinqWithEFCore;
using Microsoft.EntityFrameworkCore; //DbSet<T>
using static System.Console;

FilterAndSort();

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