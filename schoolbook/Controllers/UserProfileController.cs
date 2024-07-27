using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schoolbook.Models;
using System.Security.Claims;

namespace schoolbook.Controllers
{
    public class UserProfileController : Controller
    {
        SchoolBookEntities context=new SchoolBookEntities();
       

    //[HttpPost]
    public IActionResult Profile()
         {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                ViewBag.logining = true;
                var clam=claimUser.FindFirst(ClaimTypes.NameIdentifier);
                var user = context.Users.FirstOrDefault(x => x.Email == clam.Value);
                ViewBag.user = user;
                ViewBag.useropertions = context.Operations.Where(x => x.buyerrId == user.Id||x.SellerId==user.Id).Take(5).ToList();
                ViewBag.usersellopertionscount = context.Operations.Where(x => x.SellerId == user.Id).Count();
               // ViewBag.user = user;
                ViewBag.userbuyopertionscount = context.Operations.Where(x => x.buyerrId == user.Id ).Count();
                return View("Profile", user);
            }
            return RedirectToAction("SignIn", "SignIn");
        }
        [HttpPost]
        [Authorize]
        public IActionResult EditeProfile()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            
            if (claimUser.Identity.IsAuthenticated)
            {
                var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
                var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
                ViewBag.user = use;
                ViewBag.logining = true;
            }
            return View("EditeProfile");
        }
    }
}
