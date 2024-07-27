using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using schoolbook.Models;
using schoolbook.viewmodel;
using System.Security.Claims;
using Microsoft.AspNetCore.Session;
using Microsoft.CodeAnalysis;

namespace schoolbook.Controllers
{
    public class CartController : Controller
    {
        SchoolBookEntities context = new SchoolBookEntities();
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.logining = true;
            
            var cartItems = HttpContext.Session.GetObjectFromJson<List<Cartitem>>("Cart") ?? new List<Cartitem>();
            ViewBag.TotalCost = 0;
            foreach (Cartitem item in cartItems)
            {
                ViewBag.TotalCost += item.BookPrice;
            }
            ClaimsPrincipal claimUser = HttpContext.User;
            var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
            var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
            ViewBag.user = use;
            return View("cart",cartItems);
        }
        [Authorize]
        public IActionResult Addbooktocart(int id)
        {
            ViewBag.logining = true;
            Book book = context.Books.FirstOrDefault(x => x.Id == id);
            if(book==null)
            {
                return RedirectToAction("Showall", "ShowBook",0);
            }
           
            var cartItems = HttpContext.Session.GetObjectFromJson<List<Cartitem>>("Cart") ?? new List<Cartitem>();
            var existingItem = cartItems.FirstOrDefault(item => item.BookId == id);
            
            if(existingItem == null)
            {
              
                cartItems.Add(new Cartitem
                {
                    BookId = id,
                    Quantatiy = 1,
                    BookName = book.CompanyName
             ,
                    BookPrice = book.price,
                    BookQuality = book.qulaity
                    ,
                    OwnerId=book.UserId
                });
            }
            ClaimsPrincipal claimUser = HttpContext.User;
            var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
            var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
            ViewBag.user = use;
            HttpContext.Session.SetObjectAsJson("Cart", cartItems);
            return RedirectToAction("ShowAll","ShowBook",0);
        }
        [Authorize]
        public IActionResult DeletebooKfromcart(int id)
        {
            ViewBag.logining = true;
            var cartItems = HttpContext.Session.GetObjectFromJson<List<Cartitem>>("Cart") ?? new List<Cartitem>();
            var existingItem = cartItems.FirstOrDefault(item => item.BookId == id);

            if (existingItem != null)
            {
                //ViewBag.TotalCost -= existingItem.BookPrice;
                cartItems.Remove(existingItem);
            }
            ClaimsPrincipal claimUser = HttpContext.User;
            var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
            var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
            ViewBag.user = use;
            HttpContext.Session.SetObjectAsJson("Cart", cartItems);
            return View("cart", cartItems);
        }
        [Authorize]
        public IActionResult Checkorder()
        {
            ViewBag.logining = true;
            var cartItems = HttpContext.Session.GetObjectFromJson<List<Cartitem>>("Cart") ?? new List<Cartitem>();
            int flag = 0;
            foreach(Cartitem item in cartItems)
            {
                Book book = context.Books.FirstOrDefault(x => x.Id == item.BookId);
                if(book.state==0||book==null)
                {
                    flag = 1;
                    cartItems.Remove(item);
                }
                if (cartItems.Count() == 0)
                    break;
            }
            ClaimsPrincipal claimUser = HttpContext.User;
            var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
            var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
            ViewBag.user = use;
            HttpContext.Session.SetObjectAsJson("Cart", cartItems);
            if (flag == 0)
            {
                
                return RedirectToAction("finishsellopertion");
            }
            else
                return View("cart", cartItems);
        }
        //public IActionResult compeletselloperation()
        //{
        //    var cartItems = HttpContext.Session.GetObjectFromJson<List<Cartitem>>("Cart") ?? new List<Cartitem>();
        //    foreach (var item in cartItems)
        //    {
        //        Book book = context.Books.FirstOrDefault(x => x.Id == item.BookId);
        //        book.state = 0;
        //    }
        //    return RedirectToAction("finishsellopertion");
        //}
        [Authorize]
        public IActionResult finishsellopertion()
        {
            ViewBag.logining = true;
            ClaimsPrincipal claimUser = HttpContext.User;
            var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
            var buyer = context.Users.FirstOrDefault(x => x.Email == clam.Value);
            var cartItems = HttpContext.Session.GetObjectFromJson<List<Cartitem>>("Cart") ?? new List<Cartitem>();
            List<Book> collectbook = new List<Book>();
            List<Operation> collectopertion = new List<Operation>();
            ViewBag.user = buyer;
            foreach (Cartitem item in cartItems)
            {
                Book book = context.Books.FirstOrDefault(x => x.Id == item.BookId);
                if (book == null)
                {
                    foreach(Book bo in collectbook)
                    {
                        bo.state = 1;
                    }
                    return RedirectToAction("Checkorder");
                }
                var seller= context.Users.FirstOrDefault(x => x.Id == book.UserId);
                Operation operation = new Operation();
                operation.OperationData = DateTime.Now; operation.SellerId = seller.Id; operation.buyerrId = buyer.Id;
                operation.BookId = book.Id;operation.count = 1;
                collectopertion.Add(operation);
                context.Operations.Add(operation);
                context.SaveChanges();
                //operation.OperationData = DateTime.Now; 
                //operation.BookId = book.Id; operation.count = 1;
                //collectopertion.Add(operation);
                //context.Operations.Add(operation);
                //context.SaveChanges();
                book.state = 0;
                //collectbook.Add(book);
                //context.Books.Remove(book);
                //context.SaveChanges();
            }
            //foreach (Book bo in collectbook)
            //{
            //    context.Books.Remove(bo);
            //    context.SaveChanges();
            //}
            //foreach (Operation op in collectopertion)
            //{
            //    context.Operations.Add(op);
            //    context.SaveChanges();
            //}
            context.SaveChanges();
            
            return RedirectToAction("ShowAll","ShowBook",0);
        }
    }
}
