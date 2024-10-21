using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Features.Products.DeleteProduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Inventory_Management_System.VerticalSlice.Features.Products.DeleteProduct
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteProductEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;
        public DeleteProductEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
           var result = await _mediator.Send(new DeleteProductByIdCommand(id));
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }
    }
}
