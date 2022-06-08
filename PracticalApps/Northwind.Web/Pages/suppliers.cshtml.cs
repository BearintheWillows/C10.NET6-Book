using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.Web.Pages
{
    public class SuppliersModel : PageModel
    {
        public IEnumerable<Supplier>? Suppliers { get; set; }
        private NorthwindContext db;
        
        public SuppliersModel(NorthwindContext injectedContext)
        {
            db = injectedContext;
        }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind B2B Suppliers";

            Suppliers = db.Suppliers
                          .OrderBy( c => c.Country )
                          .ThenBy( c => c.CompanyName );
        }
    }
}
