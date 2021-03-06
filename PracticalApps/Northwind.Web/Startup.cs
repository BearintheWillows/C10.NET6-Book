namespace Northwind.Web;

using Packt.Shared;
using static Console;

public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddRazorPages();
		services.AddNorthwindContext();
	}
	public void Configure(
		IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (!env.IsDevelopment())
		{
			app.UseHsts();
		}
		app.UseRouting(); // start endpoint routing
		app.UseHttpsRedirection();
		app.UseDefaultFiles(); // index.html. default.html etc
		app.UseStaticFiles(); // wwwroot
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapRazorPages();
			endpoints.MapGet("/hello", () => "Hello World!");
		});
	}
}