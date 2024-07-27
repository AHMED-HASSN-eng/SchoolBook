using Microsoft.Build.Framework;

namespace schoolbook.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Subject { get; set;}
        [Required]
        public string Description { get; set;}
        [Required]
        public int Semseter { get; set;}
        [Required]
        public string PublishYear { get; set;}
        public string? StudyYear { get; set;}   
        public int qulaity { get; set;}
        public int price { get; set; }
        public int state { get; set; }
        public List<BookImage> ?BookImages { get; set;}
        public int UserId { get; set;}
    }
}
