using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;
using Inventory_Management_System.VerticalSlice.Entities;
using Inventory_Management_System.VerticalSlice.Features.Common.SharedProducts.Queries;
using MediatR;

namespace Inventory_Management_System.VerticalSlice.Features.Reports.CreateLowStockReport.Commands
{
    public record CreateLowStockReportCommand(int categoryId):IRequest<ResultDto<IEnumerable<CreateLowStockReportEndPointResponse>>>;
    public class CreateLowStockReportCommandHandler : IRequestHandler<CreateLowStockReportCommand, ResultDto<IEnumerable<CreateLowStockReportEndPointResponse>>>
    {
        private readonly IMediator _mediator;
        public CreateLowStockReportCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ResultDto<IEnumerable<CreateLowStockReportEndPointResponse>>> Handle(CreateLowStockReportCommand request, CancellationToken cancellationToken)
        {
            var lowStockProductsResult=await _mediator.Send(new CheckLowStockProductsForSpecificCategoryQuery(request.categoryId));
            if (!lowStockProductsResult.IsSuccess) 
            {
                return ResultDto<IEnumerable<CreateLowStockReportEndPointResponse>>.Faliure(lowStockProductsResult.ErrorCode, lowStockProductsResult.Message);
            }
            var lowStockProducts = lowStockProductsResult.Data.MapOne<IEnumerable<CreateLowStockReportEndPointResponse>>();


            return ResultDto<IEnumerable<CreateLowStockReportEndPointResponse>>.Sucess(lowStockProducts);
        }
    }

}
