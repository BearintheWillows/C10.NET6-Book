using Microsoft.AspNetCore.Identity; //RoleManager, UserManager
using Microsoft.AspNetCore.Mvc; // Controller, IActionResult
using static System.Console;


namespace Northwind.Mvc.Controllers; 

public class RolesController: Controller {
	public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) {
		_roleManager = roleManager;
		_userManager = userManager;
	}
	private          string                    AdminRole { get; set; } = "Administrators";
	private          string                    UserEmail { get; set; } = "test@email.com";
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly UserManager<IdentityUser> _userManager;

	public async Task<IActionResult> Index() {
		if ( !(await _roleManager.RoleExistsAsync( AdminRole )) ) {
			await _roleManager.CreateAsync( new IdentityRole( AdminRole ) );
		}

		IdentityUser user = await _userManager.FindByEmailAsync( UserEmail );

		if ( user == null ) {
			user = new();
			user.UserName = UserEmail;
			IdentityResult result = await _userManager.CreateAsync( user, "Pa$$w0rd" );
			if ( result.Succeeded ) {
				WriteLine($"User {user.UserName} created successfully");
			} else {
				foreach ( var item in result.Errors ) {
					WriteLine("Error");
				}
			}
		}

		if ( !user.EmailConfirmed ) {
			string token = await _userManager.GenerateEmailConfirmationTokenAsync( user );
			IdentityResult result = await _userManager.ConfirmEmailAsync( user, token );
			if ( result.Succeeded ) {
				WriteLine($"User: {user.UserName} email confirmed.");
			} else {
				foreach ( var item in result.Errors ) {
					WriteLine(Error);
				}
			}
		}

		if ( !( await _userManager.IsInRoleAsync( user, AdminRole ) ) ) {
			IdentityResult result = await _userManager.AddToRoleAsync( user, AdminRole );
			if ( result.Succeeded ) {
				WriteLine($"User: {user.UserName} added to {AdminRole} successfully");
			} else {
				foreach ( var item in result.Errors ) {
					WriteLine(Error);
				}
			}
		}

		return Redirect( "/" );
	}
}