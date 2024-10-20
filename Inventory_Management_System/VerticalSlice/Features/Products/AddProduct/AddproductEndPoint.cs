using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;
using Inventory_Management_System.VerticalSlice.Features.Products.AddProduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.VerticalSlice.Features.Products.AddProduct
{
    [ApiController]
    [Route("[controller]")]
    public class AddproductEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public AddproductEndPoint(IMediator mediator)
        {
             _mediator =mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] AddProductEndPointRequest request)
        {
           var result = await _mediator.Send(request.MapOne<AddProductCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode,result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));  
        }
    }
}
