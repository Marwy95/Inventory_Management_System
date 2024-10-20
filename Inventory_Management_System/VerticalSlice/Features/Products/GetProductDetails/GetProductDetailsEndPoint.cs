using Azure.Core;
using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Features.Products.AddProduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.VerticalSlice.Features.Products.GetProductDetails.Queries;

namespace Inventory_Management_System.VerticalSlice.Features.Products.GetProductDetails
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GetProductDetailsEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public GetProductDetailsEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetProductDetailsByIdQuery(id));
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<GetProductDetailsResponse>.Sucess(result.Data, result.Message));
        }
    }
}
