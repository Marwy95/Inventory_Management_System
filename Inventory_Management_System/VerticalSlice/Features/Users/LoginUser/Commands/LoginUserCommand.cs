using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Entities;
using MediatR;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Common.Services.TokenService;

namespace Inventory_Management_System.VerticalSlice.Features.Users.LoginUser.Commands
{
        public record LoginUserCommand(string Email, string Password) : IRequest<ResultDto<string>>;

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultDto<string>>
        {
            private readonly IBaseRepository<User> _userRepository;
            private readonly IMediator _mediator;
            private readonly ITokenService _tokenService;

            public LoginUserCommandHandler(IBaseRepository<User> userRepository, IMediator mediator, ITokenService tokenService)
            {
                _userRepository = userRepository;
                _mediator = mediator;
                _tokenService = tokenService;
            }
            public async Task<ResultDto<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var user = _userRepository.First(c => c.Email == request.Email);

                if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password) )
                {
                    return ResultDto<string>.Faliure(ErrorCode.WrongPasswordOrEmail, "Email or Password is incorrect");
                }
                user.LastLogin = DateTime.UtcNow;
                _userRepository.Update(user);
                _userRepository.SaveChanges();
                var token = await _tokenService.GenerateToken(user);
                if (!token.IsSuccess)
                {
                    return ResultDto<string>.Faliure(token.ErrorCode, token.Message);
                }
                return ResultDto<string>.Sucess<string>(token.Data, "User Login Successfully!");
            }
        }
    }

