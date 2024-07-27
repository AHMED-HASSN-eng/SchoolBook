using Microsoft.Build.Framework;

namespace schoolbook.Models
{
    public class BookImage
    {
        public int Id { get; set; }
        public string bookimage { get; set; }
        public int BookId { get; set; }
    }
}
