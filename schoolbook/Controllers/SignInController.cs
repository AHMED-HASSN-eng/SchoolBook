using Microsoft.AspNetCore.Mvc;
using schoolbook.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AuthProject.Models;
namespace schoolbook.Controllers
{
    public class SignInController : Controller
    {
        SchoolBookEntities context=new SchoolBookEntities();
        public IActionResult SignIn()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
               // var clam=claimUser.FindFirst(ClaimTypes.NameIdentifier);
                return RedirectToAction("Profile", "UserProfile");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckSignin(VMLogin modelLogin)
        {
            var user=context.Users.FirstOrDefault(x => x.Email == modelLogin.Email);
            if (user == null)
            {
                return RedirectToAction("SignIn");
            }
            else if(user.Password == modelLogin.Password)
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties","Example Role")

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Profile","UserProfile");
            }

            return RedirectToAction("SignIn");
        }
    }
}
