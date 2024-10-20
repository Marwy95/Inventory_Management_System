using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Entities;
using Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Commands;
using Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Queries;
using MediatR;

namespace Inventory_Management_System.VerticalSlice.Features.Transactions.RemoveStockTransaction.Commands
{
    
    public record RemoveStockTransactionCommand(int ProductId, int Quantity, int UserId) : IRequest<ResultDto<int>>;

    public class RemoveStockTransactionCommandHandler : IRequestHandler<RemoveStockTransactionCommand, ResultDto<int>>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<Transaction> _transactionRepository;
        public RemoveStockTransactionCommandHandler(IMediator mediator, IBaseRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
            _mediator = mediator;
        }
        public async Task<ResultDto<int>> Handle(RemoveStockTransactionCommand request, CancellationToken cancellationToken)
        {
            var ProductResult = await _mediator.Send(new GetProductByIdQuery(request.ProductId));
            if (!ProductResult.IsSuccess)
            {
                return ResultDto<int>.Faliure(ProductResult.ErrorCode, ProductResult.Message);
            }
            var product = ProductResult.Data;
            if (product.Quantity < request.Quantity)
            {
                return ResultDto<int>.Faliure(ErrorCode.NotEnoughProducts, "There are No Enough Products");
            }
            var transaction = new Transaction
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UserId = request.UserId,
                TransactionType = TransactionType.Remove,
                CreatedAt = DateTime.UtcNow,
            };
            var newTransaction = _transactionRepository.Add(transaction);
            _transactionRepository.SaveChanges();
            _mediator.Send(new UpdateProductQuantityByIdCommand(product.ID, product.Quantity - request.Quantity));
            return ResultDto<int>.Sucess(newTransaction.ID, "Transaction Added Successfully");
        }

    }
}
