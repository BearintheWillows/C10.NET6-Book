using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.Web.Pages;

using Microsoft.AspNetCore.Mvc;
using Packt.Shared;

public class CustomersModel : PageModel {

	public Customer?             Customer  { get; set; }
	public IEnumerable<Customer> Customers { get; set; }


	private NorthwindContext db;

	//Initialise database
	public CustomersModel(NorthwindContext injectedContext) {
		db = injectedContext;
	}

	public async Task<IActionResult> OnGetAsync() {
		ViewData[ "Title" ] = "Potential Customers at B2B Northwind";

		Customers = await GetData();
	Customer = await GetCustomerById( "BOLID" );

	Console.WriteLine(Customer.CompanyName);
	return Page();
	}

	public Task<IOrderedQueryable<Customer>>  GetData() {
		Task<IOrderedQueryable<Customer>> result = Task.FromResult( db.Customers.OrderBy( c => c.Country ).ThenBy( c => c.CompanyName ) );
		return result ;
	}

	public Task<Customer> GetCustomerById(string customerId) {
	
		Task<Customer> customer = Task.FromResult( db.Customers.Single(c => c.CustomerId == customerId) );
		
		return customer;
	}
}