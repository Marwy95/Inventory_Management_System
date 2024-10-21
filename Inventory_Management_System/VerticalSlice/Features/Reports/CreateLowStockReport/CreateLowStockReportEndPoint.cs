using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Features.Products.GetAllProducts.Queries;
using Inventory_Management_System.VerticalSlice.Features.Products.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Queries;
using Inventory_Management_System.VerticalSlice.Features.Reports.CreateLowStockReport.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Inventory_Management_System.VerticalSlice.Features.Reports.CreateLowStockReport
{
    [ApiController]
    [Route("[controller]")]
    public class CreateLowStockReportEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public CreateLowStockReportEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateLowStockReportAsync(int categoryId)
        {
            var result = await _mediator.Send(new CreateLowStockReportCommand(categoryId));
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<CreateLowStockReportEndPointResponse>.Sucess(result.Data, result.Message));
        }
    }
}
