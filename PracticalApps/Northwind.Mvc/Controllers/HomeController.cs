using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Models;

namespace Northwind.Mvc.Controllers;

using Microsoft.AspNetCore.Authorization;

public class HomeController : Controller {
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger) {
		_logger = logger;
	}

	public IActionResult Index() {
		_logger.LogError( "Serious error! Jokes!" );
		_logger.LogWarning( "First Warning" );
		_logger.LogWarning( "Second Warning!" );
		_logger.LogInformation( "Index Method of Home Controller" );
		
		return View();
	}

	[Authorize(Roles = "Administrators")]
	public IActionResult Privacy() {
		return View();
	}

	[ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
	public IActionResult Error() {
		return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
	}
}