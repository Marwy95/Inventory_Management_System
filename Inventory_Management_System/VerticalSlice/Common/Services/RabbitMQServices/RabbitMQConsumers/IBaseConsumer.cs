using Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQMessages;

namespace Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQConsumers
{
    public interface IBaseConsumer<T> where T : RabbitMQBaseMessage
    {
        Task Consumer(T message);
    }
}
