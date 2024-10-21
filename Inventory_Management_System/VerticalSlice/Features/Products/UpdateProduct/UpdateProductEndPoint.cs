using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Features.Products.AddProduct.Commands;
using Inventory_Management_System.VerticalSlice.Features.Products.AddProduct;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Inventory_Management_System.VerticalSlice.Features.Products.UpdateProduct.Commands;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;
using Microsoft.AspNetCore.Authorization;

namespace Inventory_Management_System.VerticalSlice.Features.Products.UpdateProduct
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateProductEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public UpdateProductEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductEndPointRequest request)
        {
            var result = await _mediator.Send(request.MapOne<UpdateProductCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }

    }
}
