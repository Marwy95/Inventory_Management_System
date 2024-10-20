using Inventory_Management_System.VerticalSlice.Common.Services.EmailServices;
using Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQMessages;
using Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQPublishers;
using Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.VerticalSlice.Common.Services.BackgroundJobServices
{
    public class LowStockbackgroundJob
    {
        private readonly IMediator _mediator;
       // private readonly IEmailService _emailService;
        private readonly IRabbitMQPublisher _rabbitService;
        public LowStockbackgroundJob(IMediator mediator, IRabbitMQPublisher rabbitService)
        {
            _mediator = mediator;
            _rabbitService = rabbitService;
        }
        public async Task CheckLowStockProducts()
        {
            var lowStockProductsResult = await _mediator.Send(new CheckLowStockProductsQuery());
            if (!lowStockProductsResult.IsSuccess) 
            {

            }
            var lowStockProducts = lowStockProductsResult.Data;
            foreach (var product in lowStockProducts)
            {
                LowStockProductMessage baseMessage = new LowStockProductMessage
                {
                    Action = "Low Stock Product",
                    Sender = "Inventory_Management_System",
                    ProductId = product.ID,
                    ProductName = product.Name,
                    Quantity = product.Quantity,
                    LowStockThreshold = product.LowStockThreshold,
                };
                baseMessage.Type = baseMessage.GetType().Name;
                string msg = Newtonsoft.Json.JsonConvert.SerializeObject(baseMessage);
                _rabbitService.PublishMessage(msg);
            }
        }
    }
}
