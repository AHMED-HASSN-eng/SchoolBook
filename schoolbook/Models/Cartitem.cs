using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using schoolbook.viewmodel;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
namespace schoolbook.Models
{
    
    public class Cartitem
    {
        SchoolBookEntities context = new SchoolBookEntities();
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int BookPrice { get; set; }
        public int BookQuality { get; set; }
        public int OwnerId { get; set; }
        public int Quantatiy { get; set; }

        
    }
}
