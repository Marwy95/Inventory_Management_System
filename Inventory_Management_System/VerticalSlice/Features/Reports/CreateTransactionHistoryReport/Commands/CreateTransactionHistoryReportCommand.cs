using Inventory_Management_System.VerticalSlice.Common;
using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Entities;
using MediatR;

namespace Inventory_Management_System.VerticalSlice.Features.Reports.CreateTransactionHistoryReport.Commands
{
    public record CreateTransactionHistoryReportCommand(DateTime StartDate,DateTime EndDate):IRequest<ResultDto<IEnumerable<CreateTransactionHistoryReportResponse>>>;
    public class CreateTransactionHistoryReportCommandHandler : IRequestHandler<CreateTransactionHistoryReportCommand, ResultDto<IEnumerable<CreateTransactionHistoryReportResponse>>>
    {
        private readonly IBaseRepository<Transaction> _transactionRepository;
        public CreateTransactionHistoryReportCommandHandler(IBaseRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<ResultDto<IEnumerable<CreateTransactionHistoryReportResponse>>> Handle(CreateTransactionHistoryReportCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<CreateTransactionHistoryReportResponse> transactionResult =  _transactionRepository.Get(t => t.CreatedAt >= request.StartDate & t.CreatedAt <= request.EndDate)
                .Map<CreateTransactionHistoryReportResponse>().ToList();
            if (!transactionResult.Any()) 
            { 
                return ResultDto < IEnumerable<CreateTransactionHistoryReportResponse>> .Faliure(ErrorCode.NoTransactionHistory, "No Transaction History Found in this Range");
            }
            return ResultDto<IEnumerable<CreateTransactionHistoryReportResponse>>.Sucess(transactionResult);
        }
    }
}
