using Inventory_Management_System.VerticalSlice.Common.Enums;

namespace Inventory_Management_System.VerticalSlice.Entities
{
    public class Transaction:BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
