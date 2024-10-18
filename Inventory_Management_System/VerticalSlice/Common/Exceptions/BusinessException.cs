using Inventory_Management_System.VerticalSlice.Common.Enums;

namespace Inventory_Management_System.VerticalSlice.Common.Exceptions
{
    public class BusinessException:Exception
    { 
        public ErrorCode ErrorCode { get; set; }
        public BusinessException(ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
