namespace Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQPublishers
{
    public interface IRabbitMQPublisher
    {
        void PublishMessage(string message);
    }
}
