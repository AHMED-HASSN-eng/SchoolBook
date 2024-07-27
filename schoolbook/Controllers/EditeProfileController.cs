using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using schoolbook.Models;
using schoolbook.viewmodel;
using System.Security.Claims;

namespace schoolbook.Controllers
{
    public class EditeProfileController : Controller
    {
        SchoolBookEntities context=new SchoolBookEntities();
        private readonly IWebHostEnvironment host;
        public EditeProfileController(IWebHostEnvironment host)
        {
            this.host = host;
        }
        [Authorize]
       // [HttpPost]
        public IActionResult EditeProfile()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                ViewBag.logining = true;
                var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
                var user = context.Users.FirstOrDefault(x => x.Email == clam.Value);
                ViewBag.user = user;
                return View("EditeProfile", user);
            }
            return RedirectToAction("signin","signin");
        }
        public string filename =string.Empty;
       // [HttpPost]
        [Authorize]
        public IActionResult SaveChange(User user,userviewmodel usermodel)
        {
            ViewBag.logining = true;
            ClaimsPrincipal claimUser = HttpContext.User;
            var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
            var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
           
            // User? use = context.Users.FirstOrDefault(u => u.Id == id);

            if (use.Email == user.Email|| context.Users.FirstOrDefault(u => u.Email == user.Email)==null)
            {
                if (use != null && user.FirstName != "" && user.LastName != "" && user.PhoneNumber != ""
                    && user.Email != "" && user.Governorate != "" && user.Central != "")
                {
                    if (usermodel.file != null)
                    {
                        string path = Path.Combine(host.WebRootPath, "userimages");
                        filename = usermodel.file.FileName;
                        string fullpath = Path.Combine(path, filename);
                        string oldimge = use.Image;
                        string fulloldimage = Path.Combine(path, oldimge);
                        if(oldimge!="defult.jpg")
                        System.IO.File.Delete(fulloldimage);
                        usermodel.file.CopyTo(new FileStream(fullpath, FileMode.Create));
                        use.Image = filename;
                    }
                    use.FirstName = user.FirstName; use.LastName=user.LastName;use.PhoneNumber=user.PhoneNumber;
                    use.Governorate=user.Governorate; use.Central=user.Central;use.Email=user.Email;
                    
                    context.SaveChanges();
                    ViewBag.user = use;
                    return RedirectToAction ("Profile", "UserProfile");
                }
                else
                    return RedirectToAction("EditeProfile");
            }
            else
            {
                return RedirectToAction("EditeProfile");
            }
            
        }
    }
}
