using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common.Services.EmailServices;
using Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQMessages;
using MediatR;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQConsumers
{
    public class RabbitMQConsumer : IHostedService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        public RabbitMQConsumer(IMediator mediator, IEmailService emailService)
        {
            Console.WriteLine("RabbitMQConsumers constructor");
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _mediator = mediator;
            _emailService = emailService;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("start async");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Recived;
            _channel.BasicConsume("TransactionsQueue", autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }

        private void Consumer_Recived(object? sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine("Consumer_Recived");
            try
            {
                Console.WriteLine("try Consumer_Recived");
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                var baseMessage = Getmessage(message);
                Console.WriteLine(baseMessage);
                InvokeConsumer(baseMessage);
                _channel.BasicAck(e.DeliveryTag, multiple: false);
            }
            catch (BusinessException ex)
            {
                _channel.BasicReject(e.DeliveryTag, requeue: false);
            }
            catch (Exception ex)
            {
                _channel.BasicReject(e.DeliveryTag, requeue: true);
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        private RabbitMQBaseMessage Getmessage(string message)
        {
            Console.WriteLine("get message");
            var jsonObj = Newtonsoft.Json.Linq.JObject.Parse(message);
            var typeName = jsonObj["Type"].ToString();
            var nameSpace = "Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQMessages";
            Type type = Type.GetType($"{nameSpace}.{typeName},Inventory_Management_System");
            if (type == null)
            {
                throw new Exception();
            }
            var baseMessage = Newtonsoft.Json.JsonConvert.DeserializeObject(message, type) as RabbitMQBaseMessage;
            baseMessage.Type = typeName.Replace("Message", "");
            return baseMessage;
        }
        private async Task InvokeConsumer(RabbitMQBaseMessage baseMessage)
        {
            Console.WriteLine("InvokeConsumer");
            var typeName = $"{baseMessage.Type}Consumer";
            var nameSpace = "Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQConsumers";
            var consumerType = Type.GetType($"{nameSpace}.{typeName},Inventory_Management_System");
            if (consumerType == null)
            {
                throw new Exception();
            }
            var consumer = Activator.CreateInstance(consumerType, _mediator, _emailService);
            var methode = consumerType.GetMethod("Consumer");
            methode.Invoke(consumer, new object[] { baseMessage });
        }
    }
}
