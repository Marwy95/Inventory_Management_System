using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Entities;
using MediatR;
using Inventory_Management_System.VerticalSlice.Data.Repositories;

namespace Inventory_Management_System.VerticalSlice.Features.Common.SharedUsers.Queries
{
    public record GetAdminByRoleQuery : IRequest<ResultDto<User>>;

    public class GetAdminByRoleQueryHandler : IRequestHandler<GetAdminByRoleQuery, ResultDto<User>>
    {
        private readonly IBaseRepository<User> _userRepository;

        public GetAdminByRoleQueryHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultDto<User>> Handle(GetAdminByRoleQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.First(u => u.Role == Role.Admin);
            if (user is null)
            {
                return ResultDto<User>.Faliure(ErrorCode.EmailIsNotFound, "Admin is Not Found");
            }
            return ResultDto<User>.Sucess(user);
        }
    }
}
