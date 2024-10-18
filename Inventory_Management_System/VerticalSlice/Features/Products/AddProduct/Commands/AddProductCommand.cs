using MediatR;

namespace Inventory_Management_System.VerticalSlice.Features.Products.AddProduct.Commands
{
    public record AddProductCommand():IRequest<string>;
    public class AddproductCommandHandler : IRequestHandler<AddProductCommand, string>
    {
        public async Task<string> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            //Console.WriteLine("hello from cqrs");
            return "hello from cqrs";
        }
    }

}
