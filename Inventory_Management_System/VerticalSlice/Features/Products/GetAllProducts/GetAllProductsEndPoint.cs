using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Features.Products.GetProductDetails.Queries;
using Inventory_Management_System.VerticalSlice.Features.Products.GetProductDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.VerticalSlice.Features.Products.GetAllProducts.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Inventory_Management_System.VerticalSlice.Features.Products.GetAllProducts
{
    [ApiController]
    [Route("[controller]")]
    public class GetAllProductsEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public GetAllProductsEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<GetAllProductsEndPointResponse>.Sucess(result.Data, result.Message));
        }
    }
}
