using AutoMapper;
using Inventory_Management_System.VerticalSlice.Entities;
using Inventory_Management_System.VerticalSlice.Features.Products.AddProduct;
using Inventory_Management_System.VerticalSlice.Features.Products.AddProduct.Commands;
using Inventory_Management_System.VerticalSlice.Features.Products.GetAllProducts;
using Inventory_Management_System.VerticalSlice.Features.Products.GetProductDetails;
using Inventory_Management_System.VerticalSlice.Features.Products.UpdateProduct;
using Inventory_Management_System.VerticalSlice.Features.Products.UpdateProduct.Commands;

namespace Inventory_Management_System.VerticalSlice.Common.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<AddProductEndPointRequest,AddProductCommand>();
            CreateMap<AddProductCommand, Product>();

            CreateMap<Product, GetProductDetailsResponse>().ReverseMap();

            CreateMap<UpdateProductEndPointRequest, UpdateProductCommand>();
            CreateMap<Product, GetAllProductsEndPointResponse>().ReverseMap();
        }
    }
}
