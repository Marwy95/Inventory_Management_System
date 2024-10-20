using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Entities;
using Inventory_Management_System.VerticalSlice.Features.Products.GetProductDetails;
using MediatR;

namespace Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Queries
{
  public record GetProductByIdQuery(int id) : IRequest<ResultDto<Product>>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ResultDto<Product>>
    {
        private readonly IBaseRepository<Product> _productRepository;

        public GetProductByIdQueryHandler(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResultDto<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetById(request.id);

            if (product is null)
            {
                throw new BusinessException(ErrorCode.InvalidProductID, "Product is Not Found");
            }
            return ResultDto<GetProductDetailsResponse>.Sucess(product);
        }
    }
}
