using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Entities;
using Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Queries;

using MediatR;

namespace Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Commands
{
    public record UpdateProductQuantityByIdCommand(int Id, int Quantity) : IRequest<ResultDto<bool>>;
    public class UpdateProductQuantityByIdCommandHandler : IRequestHandler<UpdateProductQuantityByIdCommand, ResultDto<bool>>
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IMediator _mediator;
        public UpdateProductQuantityByIdCommandHandler(IBaseRepository<Product> productRepository, IMediator mediator)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }
        public async Task<ResultDto<bool>> Handle(UpdateProductQuantityByIdCommand request, CancellationToken cancellationToken)
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
            product.Quantity = request.Quantity;
            _productRepository.Update(product);
            _productRepository.SaveChanges();
            return ResultDto<bool>.Sucess(true);
        }
    }
}
