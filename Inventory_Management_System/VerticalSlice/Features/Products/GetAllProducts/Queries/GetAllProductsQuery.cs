using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Entities;
using Inventory_Management_System.VerticalSlice.Features.Products.GetProductDetails;
using MediatR;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;

namespace Inventory_Management_System.VerticalSlice.Features.Products.GetAllProducts.Queries
{
        public record GetAllProductsQuery() : IRequest<ResultDto<IEnumerable<GetAllProductsEndPointResponse>>>;

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ResultDto<IEnumerable<GetAllProductsEndPointResponse>>>
    {
        private readonly IBaseRepository<Product> _productRepository;

        public GetAllProductsQueryHandler(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResultDto<IEnumerable<GetAllProductsEndPointResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetAllProductsEndPointResponse> products = _productRepository.GetAll().Map<GetAllProductsEndPointResponse>().ToList();

            if (products is null)
            {
                throw new BusinessException(ErrorCode.NoProductsFound, "No products Found");
            }
            return ResultDto<IEnumerable<GetAllProductsEndPointResponse>>.Sucess(products);
        }
    }
}
