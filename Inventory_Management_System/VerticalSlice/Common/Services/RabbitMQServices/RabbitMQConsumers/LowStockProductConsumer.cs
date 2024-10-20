using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common.Services.EmailServices;
using Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQMessages;
using Inventory_Management_System.VerticalSlice.Features.Common.SharedUsers.Queries;
using MediatR;

namespace Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQConsumers
{
    public class LowStockProductConsumer : IBaseConsumer<LowStockProductMessage>
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        public LowStockProductConsumer(IMediator mediator, IEmailService emailService)
        {
            _mediator = mediator;
            _emailService = emailService;
        }
        public async Task Consumer(LowStockProductMessage message)
        {
            var result = await _mediator.Send(new GetAdminByRoleQuery());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            var admin = result.Data;
            var body = $@"
                              Hello,
                              This Email is from {message.Sender} 
                              This Product {message.ProductName} With Id: {message.ProductId} and Quantity:{message.Quantity}
                              it's Low Stock Threshold is {message.LowStockThreshold}
                              needs to be restocked.";
            var email = new SendEmailDto(admin.Email, message.Sender, body);
            var sendEmailResult = await _emailService.SendEmailAsync(email);
            if (!sendEmailResult.IsSuccess)
            {
                throw new BusinessException(sendEmailResult.ErrorCode, sendEmailResult.Message);
            }
        }
    }
}
