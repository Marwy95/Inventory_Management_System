using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;

namespace Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterUserEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;

        public RegisterUserEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserEndPointRequest request)
        {
            var result = await _mediator.Send(request.MapOne<RegisterUserCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }
    }
}
