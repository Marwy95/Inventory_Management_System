using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Features.Transactions.AddStockTransaction.Command;
using Inventory_Management_System.VerticalSlice.Features.Transactions.AddStockTransaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.VerticalSlice.Features.Transactions.RemoveStockTransaction.Commands;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;

namespace Inventory_Management_System.VerticalSlice.Features.Transactions.RemoveStockTransaction
{
    [ApiController]
    [Route("[controller]")]
    public class RemoveStockTransactionEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;
        public RemoveStockTransactionEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddStockTransactionAsync([FromBody] RemoveStockTransactionEndPointRequest request)
        {
            var result = await _mediator.Send(request.MapOne<RemoveStockTransactionCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }
    }
}
