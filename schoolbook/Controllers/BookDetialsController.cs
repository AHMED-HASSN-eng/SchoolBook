using Microsoft.AspNetCore.Mvc;
using schoolbook.Models;
using System.Security.Claims;

namespace schoolbook.Controllers
{
    public class BookDetialsController : Controller
    {
        SchoolBookEntities context=new SchoolBookEntities();
        public IActionResult Index(int id)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            ViewBag.logining = false;
            Book book = context.Books.FirstOrDefault(b => b.Id == id);
            User user = context.Users.FirstOrDefault(u => u.Id == book.UserId);
            if (claimUser.Identity.IsAuthenticated)
            {
                ViewBag.logining = true;
                ViewBag.user = user;
            }
            
           
            ViewBag.username = $"{user.FirstName}  {user.LastName}";
            return View("showDetials",book);
        }
    }
}
