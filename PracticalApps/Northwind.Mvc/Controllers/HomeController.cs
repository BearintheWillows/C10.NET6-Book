using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Models;
using Packt.Shared;

namespace Northwind.Mvc.Controllers;

using Microsoft.AspNetCore.Authorization;

public class HomeController : Controller {
	private readonly ILogger<HomeController> _logger;
	private readonly NorthwindContext        db;

	public HomeController(ILogger<HomeController> logger, NorthwindContext db) {
		_logger = logger;
		this.db = db;
	}

	[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
	public IActionResult Index() {
		_logger.LogError( "Serious error! Jokes!" );
		_logger.LogWarning( "First Warning" );
		_logger.LogWarning( "Second Warning!" );
		_logger.LogInformation( "Index Method of Home Controller" );

		HomeIndexViewModel model = new (VisitorCount: ( new Random() ).Next( 1, 1001 ),
		                               Categories: db.Categories.ToList(),
		                               Products: db.Products.ToList()
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
}