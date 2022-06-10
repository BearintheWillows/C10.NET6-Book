using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.Web.Pages;

using Microsoft.AspNetCore.Mvc;
using Packt.Shared;

public class CustomerDetailModel : PageModel {
	public Customer?           Customer { get; set; }
	public IQueryable<Order>? Orders   { get; set; }

	private NorthwindContext db;

	//Initialise database
	public CustomerDetailModel(NorthwindContext injectedContext) {
		db = injectedContext;
	}

	public async Task<IActionResult> OnGetAsync(string id) {

		Customer = await GetCustomerById( id );
		Orders = await GetOrdersById( id );

		return Page();
	}

	public Task<Customer> GetCustomerById(string customerId) {

		Task<Customer> customer = Task.FromResult( db.Customers.Single( c => c.CustomerId == customerId ) );

		return customer;
	}

	public Task<IQueryable<Order>> GetOrdersById(string customerId) {

		Task<IQueryable<Order>> orders = Task.FromResult( db.Orders.Where( c => c.CustomerId == customerId ) );

		return orders;
	}
}