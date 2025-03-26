using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public int SaleNumber { get; set; } = 0;
        public Guid CustomerId { get; set; } = Guid.NewGuid();
        public Guid BranchId { get; set; } = Guid.NewGuid();
        public List<SaleItem> Items {  get; set; } = new List<SaleItem>();

    }
}
