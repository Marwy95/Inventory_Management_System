namespace Inventory_Management_System.VerticalSlice.Common.Services.EmailServices
{
    public interface IEmailService
    {
        Task<ResultDto<bool>> SendEmailAsync(SendEmailDto payload);
    }
}
