namespace Inventory_Management_System.VerticalSlice.Features.Reports.CreateLowStockReport
{
    public class CreateLowStockReportEndPointResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int LowStockThreshold { get; set; }
        public string CategoryName { get; set; }
    }
}
