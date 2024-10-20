using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Common;
using MediatR;
using Inventory_Management_System.VerticalSlice.Entities;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;
using Inventory_Management_System.VerticalSlice.Common.Exceptions;

namespace Inventory_Management_System.VerticalSlice.Features.Products.GetProductDetails.Queries
{
    
    public record GetProductDetailsByIdQuery(int id) : IRequest<ResultDto<GetProductDetailsResponse>>;
   
    public class GetProductDetailsByIdQueryHandler : IRequestHandler<GetProductDetailsByIdQuery, ResultDto<GetProductDetailsResponse>>
    {
        private readonly IBaseRepository<Product> _productRepository;

        public GetProductDetailsByIdQueryHandler(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResultDto<GetProductDetailsResponse>> Handle(GetProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetById(request.id);

            if (product is null)
            {
               throw new BusinessException(ErrorCode.InvalidProductID, "Product is Not Found");
            }
            return ResultDto<GetProductDetailsResponse>.Sucess(product.MapOne<GetProductDetailsResponse>());
        }
    }
}
