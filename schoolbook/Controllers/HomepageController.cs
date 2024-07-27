using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using schoolbook.Models;
using System.Security.Claims;

namespace schoolbook.Controllers
{
    public class HomepageController : Controller
    {
        SchoolBookEntities context = new SchoolBookEntities();

        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
 
            
            if (claimUser.Identity.IsAuthenticated)
            {
                var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
                var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
                ViewBag.user = use;
                ViewBag.logining = true;
            }
            List<Book> somebooks = context.Books.Take(5).ToList();

            return View("Homepage",somebooks);
        }
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index");
        }
    }
}
