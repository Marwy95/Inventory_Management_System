namespace Inventory_Management_System.VerticalSlice.Entities
{
    public class ArchiveTransaction:Transaction
    {
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
