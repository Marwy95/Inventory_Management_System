using Inventory_Management_System.VerticalSlice.Common.MapperHelper;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Entities;
using MediatR;
using Inventory_Management_System.VerticalSlice.Features.Products.GetProductDetails.Queries;
using Inventory_Management_System.VerticalSlice.Features.Products.UpdateProduct.Queries;
using Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Queries;

namespace Inventory_Management_System.VerticalSlice.Features.Products.UpdateProduct.Commands
{
    public record UpdateProductCommand(int Id,string Name, string Description, int Quantity, decimal Price, int LowStockThreshold) : IRequest<ResultDto<bool>>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResultDto<bool>>
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IMediator _mediator;
        public UpdateProductCommandHandler(IBaseRepository<Product> productRepository, IMediator mediator)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }
        public async Task<ResultDto<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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
            product.Name = request.Name;
            product.Description = request.Description;
            product.Quantity = request.Quantity;
            product.Price = request.Price;
            product.LowStockThreshold = request.LowStockThreshold;
             _productRepository.Update(product);
            _productRepository.SaveChanges();
            return ResultDto<bool>.Sucess(true);
        }
    }
}
