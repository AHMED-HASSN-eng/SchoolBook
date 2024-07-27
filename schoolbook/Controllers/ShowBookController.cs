using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolbook.Models;
using System.Collections.Specialized;
using System.Security.Claims;

namespace schoolbook.Controllers
{
    public class ShowBookController : Controller
    {
        SchoolBookEntities context = new SchoolBookEntities();
        public IActionResult ShowAll(int id)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            
            if (claimUser.Identity.IsAuthenticated)
            {
                var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
                var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
                ViewBag.user = use;
                ViewBag.logining = true;
            }
            List<Book> book = new List<Book>();
            if (id == 0)
            {
                book = context.Books.Where(x => x.state != 0).ToList();
                ViewBag.id = 0;
            }
            else if (id == 1)
            {
                book = context.Books.Where(x => x.StudyYear.Contains("الابتدا")&&x.state!=0).ToList();
                ViewBag.id = 1;
            }
            else if (id == 2)
            {
                book = context.Books.Where(x => x.StudyYear.Contains("الاعداد") && x.state != 0).ToList();
                ViewBag.id = 2;
            }
            else if (id == 3)
            {
                book = context.Books.Where(x => x.StudyYear.Contains("الثانو") && x.state != 0).ToList();
                ViewBag.id = 3;
            }
            return View("ShowAll", book);
        }
        public IActionResult search(string trem)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            
            if (claimUser.Identity.IsAuthenticated)
            {
                var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
                var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
                ViewBag.user = use;
                ViewBag.logining = true;
            }
            List<Book> book = context.Books.Where(x => (x.StudyYear.Contains(trem) || x.CompanyName.Contains(trem)
            || x.Subject.Contains(trem))&& x.state != 0).ToList();
            return View("ShowAll", book);
        }
        public IActionResult search_term(int id,int trem, int price,string first, string second, string third, string fourth, string fifth, string sixth)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            
            if (claimUser.Identity.IsAuthenticated)
            {
                var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
                var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
                ViewBag.user = use;
                ViewBag.logining = true;
            }
            List<Book> book = new List<Book>();
            
            if (id == 1)
            {
                book = context.Books.Where(x => x.StudyYear.Contains("الابتدا") && x.state != 0).ToList();
                ViewBag.id = 1;
            }
            else if (id == 2)
            {
                book = context.Books.Where(x => x.StudyYear.Contains("الاعداد") && x.state != 0).ToList();
                ViewBag.id = 2;
            }
            else if (id == 3)
            {
                book = context.Books.Where(x => x.StudyYear.Contains("الثانو") && x.state != 0).ToList();
                ViewBag.id = 3;
            }
            book = book.Where(x => (x.Semseter<=((trem==0)?3:trem)) && (x.price<=((price==0)?700:price))
            &&(((first != null) ? x.StudyYear.Contains(first) : x.StudyYear.Contains("ش"))||
            ((second != null) ? x.StudyYear.Contains(second) : x.StudyYear.Contains("ش"))||
            ((third != null) ? x.StudyYear.Contains(third) : x.StudyYear.Contains("ش"))||
            ((fourth != null) ? x.StudyYear.Contains(fourth) : x.StudyYear.Contains("ش"))||
            ((fifth != null) ? x.StudyYear.Contains(fifth) : x.StudyYear.Contains("ش"))||
            ((sixth != null) ? x.StudyYear.Contains(sixth) : x.StudyYear.Contains("ش")))&& (x.state != 0)).ToList();
            
            return View("ShowAll", book);
        }
    }
}
