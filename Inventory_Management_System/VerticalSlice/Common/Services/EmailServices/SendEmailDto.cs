namespace Inventory_Management_System.VerticalSlice.Common.Services.EmailServices
{
    public record SendEmailDto(
       string To,
       string Subject,
       string Body,
       string? CC = null);
}
