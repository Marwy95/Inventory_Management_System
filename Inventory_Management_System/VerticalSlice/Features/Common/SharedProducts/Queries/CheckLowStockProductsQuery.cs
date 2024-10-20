using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Entities;
using MediatR;

namespace Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Queries
{
    public record CheckLowStockProductsQuery:IRequest<ResultDto<IEnumerable<Product>>>;
    public class CheckLowStockProductsQueryHandler : IRequestHandler<CheckLowStockProductsQuery, ResultDto<IEnumerable<Product>>>
    {
        private readonly IBaseRepository<Product> _productRepository;
        public CheckLowStockProductsQueryHandler(IBaseRepository<Product> productRepository)
        {
            _productRepository=productRepository;
        }
        public async Task<ResultDto<IEnumerable<Product>>> Handle(CheckLowStockProductsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable < Product > lowStockProducts = _productRepository.Get(p => p.Quantity <= p.LowStockThreshold).ToList();
            if(lowStockProducts is null)
            {
                return ResultDto<IEnumerable<Product>>.Faliure(ErrorCode.NoLowStockProducts, "No Low Stock Product Found");
            }
            return ResultDto<IEnumerable<Product>>.Sucess(lowStockProducts);
        }
    }
}
