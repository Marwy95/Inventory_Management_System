using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Entities;
using Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Queries;
using MediatR;

namespace Inventory_Management_System.VerticalSlice.Features.Products.DeleteProduct.Commands
{
    public record DeleteProductByIdCommand(int Id):IRequest<ResultDto<bool>>;
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand,ResultDto<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<Product> _productRepository;
        public DeleteProductByIdCommandHandler(IMediator mediator, IBaseRepository<Product> productRepository)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }

        public async Task<ResultDto<bool>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var existingProductResult = await _mediator.Send(new GetProductByIdQuery(request.Id));
            if (!existingProductResult.IsSuccess)
            {
                return ResultDto<bool>.Faliure(existingProductResult.ErrorCode, existingProductResult.Message);
            }
            var product = existingProductResult.Data;
            if (product is null)
            {
                return ResultDto<bool>.Faliure(existingProductResult.ErrorCode, existingProductResult.Message);
            }
            _productRepository.Delete(product);
            _productRepository.SaveChanges();
            return ResultDto<bool>.Sucess(true);
        }
    }

}
