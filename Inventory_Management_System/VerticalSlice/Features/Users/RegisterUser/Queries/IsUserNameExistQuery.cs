using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Entities;
using MediatR;
using Inventory_Management_System.VerticalSlice.Data.Repositories;

namespace Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser.Queries
{
    public record IsUserNameExistQuery(string userName) : IRequest<ResultDto<bool>>;
    public class IsUserNameExistQueryHandler : IRequestHandler<IsUserNameExistQuery, ResultDto<bool>>
    {
        private readonly IBaseRepository<User> _userRepository;
        public IsUserNameExistQueryHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultDto<bool>> Handle(IsUserNameExistQuery request, CancellationToken cancellationToken)
        {
            var result = _userRepository.Any(u => u.UserName == request.userName);
            if (result)
            {
                return ResultDto<bool>.Sucess(true);
            }
            return ResultDto<bool>.Faliure(ErrorCode.UserNameIsNotFound, "User Name is Not Found");
        }


    }
}
