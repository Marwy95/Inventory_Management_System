using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Common.Exceptions;

namespace Inventory_Management_System.VerticalSlice.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {

        RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                ErrorCode errorCode = ErrorCode.UnKnown;

                if (ex is BusinessException businessException)
                {
                    message = businessException.Message;
                    errorCode = businessException.ErrorCode;
                }
                //else
                //{
                //    File.WriteAllText("D:\\Log.txt", $"Error happened: {ex.Message}");
                //}

                var result = ResultViewModel<bool>.Faliure(errorCode, message);

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
