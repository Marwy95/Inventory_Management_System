using Inventory_Management_System.VerticalSlice.Common.Enums;

namespace Inventory_Management_System.VerticalSlice.Entities
{
    public class AddStockTransaction:Transaction
    {
        public TransactionType TransactionType { get; set; }
    }
}
