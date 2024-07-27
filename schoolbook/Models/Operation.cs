using System.Runtime.InteropServices;

namespace schoolbook.Models
{
    public class Operation
    {
        
        public int Id { get; set; }
        public DateTime OperationData { get; set; }
        public int SellerId { get; set; }
        public int buyerrId { get; set; }
        public int count { get; set; }
        public int BookId { get; set; }
    }
}
