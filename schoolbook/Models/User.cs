using Microsoft.Build.Framework;

namespace schoolbook.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public  string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Governorate { get; set;}
        [Required]
        public string Central { get; set; }
        public string  ? Image { get; set; }
    }
}
