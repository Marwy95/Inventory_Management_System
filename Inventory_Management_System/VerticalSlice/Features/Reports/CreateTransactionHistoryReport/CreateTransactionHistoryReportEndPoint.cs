using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Entities;
using Inventory_Management_System.VerticalSlice.Features.Reports.CreateLowStockReport.Commands;
using Inventory_Management_System.VerticalSlice.Features.Reports.CreateLowStockReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;
using Inventory_Management_System.VerticalSlice.Features.Reports.CreateTransactionHistoryReport.Commands;

namespace Inventory_Management_System.VerticalSlice.Features.Reports.CreateTransactionHistoryReport
{
    [ApiController]
        [Route("[controller]")]
    public class CreateTransactionHistoryReportEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public CreateTransactionHistoryReportEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransactionHistoryReportAsync(CreateTransactionHistoryReportRequest request)
        {
            var result = await _mediator.Send(request.MapOne<CreateTransactionHistoryReportCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<CreateTransactionHistoryReportResponse>.Sucess(result.Data, result.Message));
        }
    }
}
