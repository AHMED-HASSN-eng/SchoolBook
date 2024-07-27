using Microsoft.AspNetCore.Mvc;
using schoolbook.Models;

namespace schoolbook.Controllers
{
    public class SignUpController : Controller
    {
        SchoolBookEntities context=new  SchoolBookEntities();
        public IActionResult signup()
        {

            return View("resultsignup");
        }
        public IActionResult resultsignup(User user)
        {

            User? use = context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (use==null&&user.FirstName != "" && user.LastName != "" && user.PhoneNumber != ""
                    && user.Email != "" && user.Governorate != "" && user.Central != "" && user.Password != "")
                {
                    user.Image = "defult.jpg";
                    context.Users.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("SignIn","SignIn");
                }
                return View("resultsignup");
        }
    }
}
