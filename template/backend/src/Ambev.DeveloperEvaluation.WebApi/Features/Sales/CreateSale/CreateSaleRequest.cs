using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
            public int SaleNumber { get; set; } = 0;
            public Guid CustomerId { get; set; } = Guid.NewGuid();
            public Guid BranchId { get; set; } = Guid.NewGuid();
            public List<SaleItem> Items { get; set; } = new List<SaleItem>();
    }
}
