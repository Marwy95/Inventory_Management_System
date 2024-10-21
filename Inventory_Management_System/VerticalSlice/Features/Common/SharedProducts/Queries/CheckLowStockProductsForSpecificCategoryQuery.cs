using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Entities;
using MediatR;

namespace Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Queries
{
    public record CheckLowStockProductsForSpecificCategoryQuery(int categoryId) : IRequest<ResultDto<IEnumerable<Product>>>;
    public class CheckLowStockProductsForSpecificCategoryQueryHandler : IRequestHandler<CheckLowStockProductsForSpecificCategoryQuery, ResultDto<IEnumerable<Product>>>
    {
        private readonly IBaseRepository<Product> _productRepository;
        public CheckLowStockProductsForSpecificCategoryQueryHandler(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ResultDto<IEnumerable<Product>>> Handle(CheckLowStockProductsForSpecificCategoryQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Product> lowStockProducts = _productRepository.Get(p => p.Quantity <= p.LowStockThreshold && p.CategoryId==request.categoryId).ToList();
            if (lowStockProducts is null)
            {
                return ResultDto<IEnumerable<Product>>.Faliure(ErrorCode.NoLowStockProducts, "No Low Stock Product Found");
            }
            return ResultDto<IEnumerable<Product>>.Sucess(lowStockProducts);
        }
    }
}
