using AutoMapper;
using Inventory_Management_System.VerticalSlice.Features.Transactions.AddStockTransaction;
using Inventory_Management_System.VerticalSlice.Features.Transactions.AddStockTransaction.Command;
using Inventory_Management_System.VerticalSlice.Features.Transactions.RemoveStockTransaction;
using Inventory_Management_System.VerticalSlice.Features.Transactions.RemoveStockTransaction.Commands;

namespace Inventory_Management_System.VerticalSlice.Common.Profiles
{
    public class TransactionProfile:Profile
    {
        public TransactionProfile()
        {
            CreateMap<AddStockTransactionEndPointRequest, AddStockTransactionCommand>();
            CreateMap<RemoveStockTransactionEndPointRequest, RemoveStockTransactionCommand>();
        }
    }
}
