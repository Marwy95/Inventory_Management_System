using RabbitMQ.Client;
using System.Text;

namespace Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQPublishers
{
    public class RabbitMQPublisher:IRabbitMQPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public RabbitMQPublisher()
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("TransactionsExchange", ExchangeType.Fanout, true, false);
            _channel.QueueDeclare("TransactionsQueue", durable: true,
                     exclusive: false,
                     autoDelete: false);
            _channel.QueueBind("TransactionsQueue", "TransactionsExchange", "Key1");

        }
        public void PublishMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish("TransactionsExchange", "Key1", body: body);
        }
    }
}
