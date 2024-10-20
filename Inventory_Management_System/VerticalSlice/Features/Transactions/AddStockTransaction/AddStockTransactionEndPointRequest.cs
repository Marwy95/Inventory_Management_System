using Inventory_Management_System.VerticalSlice.Common.Enums;

namespace Inventory_Management_System.VerticalSlice.Features.Transactions.AddTransaction
{
    public class AddStockTransactionEndPointRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
