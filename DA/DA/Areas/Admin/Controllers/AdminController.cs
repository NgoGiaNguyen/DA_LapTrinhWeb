using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateAdminAccount()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            var user = new IdentityUser
            {
                UserName = "luonghoaiphongst@gmail.com",
                Email = "luonghoaiphongst@gmail.com"
            };
            var result = await _userManager.CreateAsync(user, "OGust@123");
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, new List<string> { "Admin" });
                return Content("Succes");
            }
            return BadRequest("Faile");
        }
    }
}
