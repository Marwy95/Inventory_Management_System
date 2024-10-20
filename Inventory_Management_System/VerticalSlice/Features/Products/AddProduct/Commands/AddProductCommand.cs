using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Entities;
using MediatR;

namespace Inventory_Management_System.VerticalSlice.Features.Products.AddProduct.Commands
{
    public record AddProductCommand(string Name, string Description, int Quantity, decimal Price, int LowStockThreshold) :IRequest<ResultDto<int>>;

    public class AddproductCommandHandler : IRequestHandler<AddProductCommand, ResultDto<int>>
    {
        private readonly IBaseRepository<Product> _productRepository;
        public AddproductCommandHandler(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task <ResultDto<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {

            var product =  request.MapOne<Product>();
            product.Name = request.Name;
            product.Description = request.Description;
            product.Quantity = request.Quantity;
            product.Price = request.Price;
            product.LowStockThreshold = request.LowStockThreshold;

            var addedProduct = _productRepository.Add(product);
            _productRepository.SaveChanges();
            return ResultDto<int>.Sucess(addedProduct.ID);
        }
    }

}
