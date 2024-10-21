using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Entities;

namespace Inventory_Management_System.VerticalSlice.Features.Reports.CreateTransactionHistoryReport
{
    public class CreateTransactionHistoryReportRequest
    {
        
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
    }
}
