using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using schoolbook.Models;
using schoolbook.viewmodel;
using System.Security.Claims;
using Microsoft.AspNetCore.Session;
using Microsoft.CodeAnalysis;
using schoolbook.Clients;
namespace schoolbook.Controllers
{
    public class CheckoutController : Controller
    {

        [TempData]
        public string TotalAmount { get; set; } = null;

        private readonly PaypalClient _paypalClient;
        public CheckoutController(PaypalClient paypalClient)
        {
            this._paypalClient = paypalClient;
        }

      

        public IActionResult Index()
        {
            

            ViewBag.ClientId = _paypalClient.ClientId;

            try
            {
                var cartItems = HttpContext.Session.GetObjectFromJson<List<Cartitem>>("Cart") ?? new List<Cartitem>();
                ViewBag.cart = cartItems;
                ViewBag.DollarAmount = cartItems.Sum(item => item.BookPrice * item.Quantatiy);
                ViewBag.total = ViewBag.DollarAmount;
                int total = ViewBag.total;
                TotalAmount = total.ToString();
                TempData["TotalAmount"] = TotalAmount;

            }
            catch (Exception)
            {

                
            }
            return View();

        }
        public IActionResult Processing(string stripeToken,string stripeEmail)
        {
            //var optionCust = new CustomerCreateOptions
            //{
            //    Email = stripeEmail,
            //    Name = "Rizwan Yousaf",
            //    Phone = "338595119"
            //};
            //var serviceCust = new CustomerService();
            //Customer customer = serviceCust.Create(optionCust);
            //var optionsCharge = new ChargeCreateOptions
            //{
            //    Amount = Convert.ToInt64(TempData["TotalAmount"]),
            //    Currency = "USD",
            //    Description="Pet Selling amount",
            //    Source=stripeToken,
            //    ReceiptEmail=stripeEmail

            //};
            //var serviceCharge = new ChargeService();
            //Charge charge = serviceCharge.Create(optionsCharge);
            //if(charge.Status=="successded")
            //{
            //    ViewBag.AmountPaid = charge.Amount;
            //    ViewBag.Customer = customer.Name;
            //}
            return View();


        }
        [HttpPost]
        public async Task<IActionResult> Order(CancellationToken cancellationToken)
        {
            try
            {
                // set the transaction price and currency
                var price = TotalAmount;
                var currency = "USD";

                // "reference" is the transaction key
                var reference = GetRandomInvoiceNumber();// "INV002";

                var response = await _paypalClient.CreateOrder(price, currency, reference);

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
        public async Task<IActionResult> Capture(string orderId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderId);

                //var reference = response.purchase_units[0].reference_id;
                // Put your logic to save the transaction here
                // You can use the "reference" variable as a transaction key


                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
        public static string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999).ToString();
        }
        public IActionResult Success()
        {
            SchoolBookEntities context = new SchoolBookEntities();
            ViewBag.logining = true;
            ClaimsPrincipal claimUser = HttpContext.User;
            var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
            var buyer = context.Users.FirstOrDefault(x => x.Email == clam.Value);
            var cartItems = HttpContext.Session.GetObjectFromJson<List<Cartitem>>("Cart") ?? new List<Cartitem>();
            foreach (Cartitem item in cartItems)
            {
                Book book = context.Books.FirstOrDefault(x => x.Id == item.BookId);
                var seller = context.Users.FirstOrDefault(x => x.Id == book.UserId);
                Operation operation = new Operation();
                operation.OperationData = DateTime.Now; operation.SellerId = seller.Id; operation.buyerrId = buyer.Id;
                operation.BookId = book.Id; operation.count = 1;
                context.Operations.Add(operation);
                context.SaveChanges();
                book.state = 0;
                context.SaveChanges();
            }
            return View();
        }
    }
}
