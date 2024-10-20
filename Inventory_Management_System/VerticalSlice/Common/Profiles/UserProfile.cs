using AutoMapper;
using Inventory_Management_System.VerticalSlice.Entities;
using Inventory_Management_System.VerticalSlice.Features.Users.LoginUser;
using Inventory_Management_System.VerticalSlice.Features.Users.LoginUser.Commands;
using Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser;
using Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser.Commands;

namespace Inventory_Management_System.VerticalSlice.Common.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserEndPointRequest, RegisterUserCommand>();
            CreateMap<RegisterUserCommand, User>();
            CreateMap<LoginUserEndPointRequest, LoginUserCommand>();
        }
    }
}
