using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Entities;

namespace Inventory_Management_System.VerticalSlice.Features.Reports.CreateTransactionHistoryReport
{
    public class CreateTransactionHistoryReportResponse
    {
        public int ProductId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}
