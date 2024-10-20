using Inventory_Management_System.VerticalSlice.Common.Enums;

namespace Inventory_Management_System.VerticalSlice.Entities
{
    public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreationAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public Role Role { get; set; }
    }
}
