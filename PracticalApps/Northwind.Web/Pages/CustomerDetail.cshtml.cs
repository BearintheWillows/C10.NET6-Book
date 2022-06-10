using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.Web.Pages;

using Microsoft.AspNetCore.Mvc;
using Packt.Shared;

public class CustomerDetailModel  : PageModel {
	public Customer? Customer    { get; set; }
	public string    CustomerStr { get; set; }
	
	private NorthwindContext db;

	//Initialise database
	public CustomerDetailModel(NorthwindContext injectedContext) {
		db = injectedContext;
	}
	public async Task<IActionResult> OnGetAsync(string id) {


		Customer = await GetCustomerById( id );

		return Page();
	}
	
	public Task<Customer> GetCustomerById(string customerId) {
	
		Task<Customer> customer = Task.FromResult( db.Customers.Single(c => c.CustomerId == customerId) );
		
		return customer;
	}
}