using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Models;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Mvc.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class HomeController : Controller {
	private readonly ILogger<HomeController> _logger;
	private readonly NorthwindContext        db;

	public HomeController(ILogger<HomeController> logger, NorthwindContext db) {
		_logger = logger;
		this.db = db;
	}

	[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
	public async Task<IActionResult> Index() {
		_logger.LogError( "Serious error! Jokes!" );
		_logger.LogWarning( "First Warning" );
		_logger.LogWarning( "Second Warning!" );
		_logger.LogInformation( "Index Method of Home Controller" );

		HomeIndexViewModel model = new (VisitorCount: (new Random()).Next( 1, 1001 ),
		                               Categories: await db.Categories.ToListAsync(),
		                               Products:  await db.Products.ToListAsync()
		);
		
		return View(model);
	}
	
	[Route("private")]
	[Authorize(Roles = "Administrators")]
	public IActionResult Privacy() {
		return View();
	}

	[ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
	public IActionResult Error() {
		return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
	}
	
	public async Task<IActionResult> ProductDetail(int? id)
	{
		if (!id.HasValue)
		{
			return BadRequest("You must pass a product ID in the route, for example, /Home/ProductDetail/21");
		}
		Product? model = await db.Products
		                   .SingleOrDefaultAsync(p => p.ProductId == id);
		if (model == null)
		{
			return NotFound($"ProductId {id} not found.");
		}
		return View(model); // pass model to view and then return result
	}

	public IActionResult ModelBinding() {
		return View(); // the page with a form to submit
	}

	[HttpPost]
	public IActionResult ModelBinding(Thing thing) {
		//return View( thing ); //Show model bound to thing

		HomeModelBindingViewModel model = new(thing,
		                                      !ModelState.IsValid,
		                                      ModelState.Values
		                                                .SelectMany( s => s.Errors )
		                                                .Select( e => e.ErrorMessage )
		);

		return View( model );
	}
	
	public IActionResult ProductsThatCostMoreThan(decimal? price)
	{
		if (!price.HasValue)
		{
			return BadRequest("You must pass a product price in the query string, for example, /Home/ProductsThatCostMoreThan?price=50");
		}
		IEnumerable<Product> model = db.Products
		                               .Include(p => p.Category)
		                               .Include(p => p.Supplier)
		                               .Where(p => p.UnitPrice > price);
		if (!model.Any())
		{
			return NotFound(
				$"No products cost more than {price:C}.");
		}
		ViewData["MaxPrice"] = price.Value.ToString("C");
		return View(model); // pass model to view
	}


}