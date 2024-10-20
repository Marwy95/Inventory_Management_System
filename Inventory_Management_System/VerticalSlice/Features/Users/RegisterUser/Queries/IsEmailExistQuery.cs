using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Entities;
using MediatR;
using Inventory_Management_System.VerticalSlice.Data.Repositories;

namespace Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser.Queries
{
    public record IsEmailExistQuery(string email) : IRequest<ResultDto<bool>>;
    public class IsEmailExistQueryHandler : IRequestHandler<IsEmailExistQuery, ResultDto<bool>>
    {
        private readonly IBaseRepository<User> _userRepository;
        public IsEmailExistQueryHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultDto<bool>> Handle(IsEmailExistQuery request, CancellationToken cancellationToken)
        {
            var result = await Task.Run(() => _userRepository.Any(u => u.Email == request.email));
            if (result)
            {
                return ResultDto<bool>.Sucess(true);
            }
            return ResultDto<bool>.Faliure(ErrorCode.EmailIsNotFound, "Email is Not Found");
        }
    }
}
