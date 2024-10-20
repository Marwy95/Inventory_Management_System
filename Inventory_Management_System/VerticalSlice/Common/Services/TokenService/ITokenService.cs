using Inventory_Management_System.VerticalSlice.Entities;

namespace Inventory_Management_System.VerticalSlice.Common.Services.TokenService
{
    public interface ITokenService
    {
        Task<ResultDto<string>> GenerateToken(User user);
    }
}
