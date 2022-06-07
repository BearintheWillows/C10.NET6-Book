using Northwind.Web;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
	{
		webBuilder.UseStartup<Startup>();
	}).Build().Run();

Console.WriteLine(" Web Server stopped." );