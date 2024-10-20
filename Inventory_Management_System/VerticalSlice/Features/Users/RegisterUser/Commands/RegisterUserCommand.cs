using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;
using Inventory_Management_System.VerticalSlice.Common.Services.EmailServices;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Entities;
using MediatR;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser.Queries;

namespace Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser.Commands
{
    
        public record RegisterUserCommand(
       string UserName, string Password, string ConfirmPassword,
       string Email, string Phone) : IRequest<ResultDto<int>>;
        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResultDto<int>>
        {
            private readonly IBaseRepository<User> _userRepository;
            private readonly IMediator _mediator;
            //private readonly RegisterUserEndPointValidation _userValidator;
            public RegisterUserCommandHandler(IBaseRepository<User> userRepository, IMediator mediator)//, RegisterUserEndPointValidation userValidator)
            {
                _userRepository = userRepository;
                _mediator = mediator;
               // _userValidator = userValidator;
            }
            public async Task<ResultDto<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                //var result = await _userValidator.ValidateUserModel(request);
                //if (!result.IsSuccess)
                //{
                //    return ResultDto<int>.Faliure(result.ErrorCode, result.Message);
                //}
               var result = await _mediator.Send(new IsEmailExistQuery(request.Email));
                if (result.IsSuccess)
                {
                    return ResultDto<int>.Faliure(ErrorCode.EmailAlreadyExist, "Email is already Exists");
                }
                result = await _mediator.Send(new IsUserNameExistQuery(request.UserName));
                if (result.IsSuccess)
                {
                    return ResultDto<int>.Faliure(ErrorCode.UserNameAlreadyExist, "User Name is already Exists");
                }

                if (request.Password != request.ConfirmPassword)
                {
                    return ResultDto<int>.Faliure(ErrorCode.PasswordsDontMatch, "Passwords don't match");
                }
                var user = request.MapOne<User>();
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.Role = Role.User;
                _userRepository.Add(user);
                _userRepository.SaveChanges();
                return ResultDto<int>.Sucess(user.ID, "User Added Successfully");
            }
        }
    }

