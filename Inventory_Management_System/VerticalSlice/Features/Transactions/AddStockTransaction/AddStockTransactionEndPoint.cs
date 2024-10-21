using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Features.Products.AddProduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.VerticalSlice.Features.Transactions.AddStockTransaction.Command;
using Microsoft.AspNetCore.Authorization;

namespace Inventory_Management_System.VerticalSlice.Features.Transactions.AddStockTransaction
{
    [ApiController]
    [Route("[controller]")]
    public class AddStockTransactionEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddStockTransactionEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddStockTransactionAsync([FromBody] AddStockTransactionEndPointRequest request)
        {
            var result = await _mediator.Send(request.MapOne<AddStockTransactionCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }
    }
}
