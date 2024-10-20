namespace Inventory_Management_System.VerticalSlice.Features.Transactions.RemoveStockTransaction
{
    public class RemoveStockTransactionEndPointRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
