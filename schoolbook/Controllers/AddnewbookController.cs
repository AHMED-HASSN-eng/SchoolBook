using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using schoolbook.Models;
using schoolbook.viewmodel;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace schoolbook.Controllers
{
    public class AddnewbookController : Controller
    {
        private readonly SchoolBookEntities context = new SchoolBookEntities();
        private readonly IWebHostEnvironment host;
        public AddnewbookController(IWebHostEnvironment host) => this.host = host;

        [Authorize]
        public IActionResult Addnewbook()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
            var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
            if (claimUser.Identity.IsAuthenticated)
            {
                ViewBag.logining = true;
                ViewBag.user = use;
            }
            return View("Addnewbook");
        }

        public async Task<IActionResult> savebook(Book book, VMbook vmbook)
        {
            string yearstudy = $"{vmbook.vmstudyear} {vmbook.stage} ";
            book.StudyYear = yearstudy;
            book.state = 1;
            ClaimsPrincipal claimUser = HttpContext.User;
            var clam = claimUser.FindFirst(ClaimTypes.NameIdentifier);
            var use = context.Users.FirstOrDefault(x => x.Email == clam.Value);
            ViewBag.user = use;
            book.UserId = use.Id;
            book.qulaity = 4;
            context.Books.Add(book);
            context.SaveChanges();
            
            int iduser = use.Id;
            int idbook = book.Id;
            string filename = string.Empty;
            int x = 1;
            var imagePaths = new List<string>();
            var results = new List<ImageModel>();

            foreach (var item in vmbook.files)
            {
                string path = Path.Combine(host.WebRootPath, "bookimages");
                filename = $"{idbook}{iduser}{x++}.jpg";
                string fullpath = Path.Combine(path, filename);
                using (var stream = new FileStream(fullpath, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }
                BookImage bookImage = new BookImage
                {
                    BookId = idbook,
                    bookimage = filename
                };
                context.Add(bookImage);
                context.SaveChanges();
                imagePaths.Add(fullpath);
            }

            var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "evaluate_images.py");
            var startInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"{scriptPath} {string.Join(" ", imagePaths)}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            string result;
            string error;

            using (var process = Process.Start(startInfo))
            {
                result = await process.StandardOutput.ReadToEndAsync();
                error = await process.StandardError.ReadToEndAsync();
            }

            Debug.WriteLine($"Python script output: {result}");
            Debug.WriteLine($"Python script error: {error}");

            if (string.IsNullOrWhiteSpace(result))
            {
                return StatusCode(500, $"No output from Python script. Error: {error}");
            }

            JArray jsonResponse;
            try
            {
                jsonResponse = JArray.Parse(result);
            }
            catch (JsonReaderException ex)
            {
                Debug.WriteLine($"JSON parsing error: {ex.Message}");
                return StatusCode(500, $"JSON parsing error: {ex.Message}. Output: {result}");
            }

            int meanquality = 0;
            foreach (var item in jsonResponse)
            {
                meanquality += (int)item["quality_score"];
            }
            meanquality /= jsonResponse.Count();
            int final_score = meanquality;
            book.qulaity = final_score;
            context.SaveChanges();
            return View("Success");
        }
    }
}
