using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleResponse
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
