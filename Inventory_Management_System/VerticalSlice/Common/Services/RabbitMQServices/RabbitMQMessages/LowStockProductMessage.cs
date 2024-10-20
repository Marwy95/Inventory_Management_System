namespace Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQMessages
{
    public class LowStockProductMessage:RabbitMQBaseMessage
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }
    }
}
