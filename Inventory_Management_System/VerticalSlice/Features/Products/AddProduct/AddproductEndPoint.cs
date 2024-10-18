using Inventory_Management_System.VerticalSlice.Features.Products.AddProduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.VerticalSlice.Features.Products.AddProduct
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AddproductEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public AddproductEndPoint(IMediator mediator)
        {
             _mediator =mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddProductAsync()
        {
           var result = await _mediator.Send(new AddProductCommand());
            return Ok(result);  
        }
    }
}
