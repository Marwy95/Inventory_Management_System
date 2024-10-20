using Inventory_Management_System.VerticalSlice.Common.Exceptions;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Features.Users.LoginUser.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;

namespace Inventory_Management_System.VerticalSlice.Features.Users.LoginUser
{
    [ApiController]
    [Route("[controller]")]
    public class LoginUserEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginUserEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult> LoginUserAsync([FromBody] LoginUserEndPointRequest request)
        {
            var result = await _mediator.Send(request.MapOne<LoginUserCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<string>.Sucess<string>(result.Data, result.Message));
        }
    }
}
