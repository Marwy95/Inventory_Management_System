using FluentValidation;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser.Commands;

namespace Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser
{
    public class RegisterUserEndPointValidator:AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserEndPointValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(15)
                .WithMessage("User Name is required.");
            RuleFor(user => user.Email)
              .NotEmpty()
              .WithMessage("Email is required.")
              .EmailAddress()
               .WithMessage("Invalid Email Address");

            RuleFor(user => user.Phone)
                .NotEmpty()
                .Matches(@"^01[0125]\d{1,8}$")
                .WithMessage("Invalid Phone Number")
                .WithErrorCode($"{ErrorCode.InvalidPhoneNumber.ToString()}");

        }
    }
    public class RegisterUserEndPointValidation
    {
        private readonly RegisterUserEndPointValidator _userValidator;
        public RegisterUserEndPointValidation(RegisterUserEndPointValidator userValidator)
        {
            _userValidator = userValidator;
        }
        public async Task<ResultDto<bool>> ValidateUserModel(RegisterUserCommand user)
        {
            var validationResults = _userValidator.Validate(user);

            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    Console.WriteLine(error.ErrorCode);
                    ErrorCode errorCode = (ErrorCode)Enum.Parse(typeof(ErrorCode), error.ErrorCode);
                    return ResultDto<bool>.Faliure(errorCode, error.ErrorMessage);
                }
            }
            return ResultDto<bool>.Sucess(true);
        }

    }
}
