using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; }
        public int SaleNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public bool IsCanceled { get; set; }
        public List<SaleItem> Items { get; set; }
        public bool IsEligibleForDiscount { get; set; }
        public decimal TotalSaleAmount { get; set; }
    }
}
