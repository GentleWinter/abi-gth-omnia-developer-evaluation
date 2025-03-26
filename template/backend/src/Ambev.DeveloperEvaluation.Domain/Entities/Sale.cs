using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {

        private readonly List<INotification> _domainEvents = new();
        public void ClearDomainEvents() => _domainEvents.Clear();
        private Sale() { }

        public Sale(int saleNumber, DateTime createdAt, Guid customerId, Guid branchId, List<SaleItem> items = null)
        {
            SaleNumber = saleNumber;
            CreatedAt = createdAt;
            CustomerId = customerId;
            BranchId = branchId;
            Items = items;
        }
        public int SaleNumber { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid BranchId { get; private set; }
        public bool IsCanceled { get; private set; }
        public List<SaleItem> Items { get; set; }

        public bool IsEligibleForDiscount => Discount.IsEligibleForDiscount(Items.Count(i => !i.IsCanceled));

        public decimal TotalSaleAmount =>
            Items.Where(i => !i.IsCanceled).Sum(i => i.TotalItemAmount);

        public void AddItem(SaleItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem != null)
            {
                var updatedItem = new SaleItem(
                    existingItem.ProductId,
                    existingItem.Quantity + item.Quantity,
                    existingItem.UnitPrice,
                    existingItem.IsEligibleForDiscount,
                    existingItem.IsCanceled,
                    () => Items.Count(i => !i.IsCanceled));

                Items.Remove(existingItem);
                Items.Add(updatedItem);
            }
            else
            {
                Items.Add(new SaleItem(
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice,
                    item.IsEligibleForDiscount,
                    item.IsCanceled,
                    () => Items.Count(i => !i.IsCanceled) + (!item.IsCanceled ? 1 : 0)));
            }

            foreach (var saleItem in Items)
            {
                saleItem.RecalculateAmounts();
            }
        }

        private void UpdateAllItems()
        {
            foreach (var item in Items)
            {
                item.RecalculateAmounts();
            }
        }

        public void RemoveItem(SaleItem item)
        {
            Items.Remove(item);
        }

        public void CancelSale()
        {
            IsCanceled = true;
            foreach (var item in Items)
            {
                var updatedItem = new SaleItem(
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice,
                    false,
                    true,
                    () => Items.Count(i => !i.IsCanceled));

                Items[Items.IndexOf(item)] = updatedItem;
            }
        }

        public void CancelSaleItem(SaleItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!Items.Contains(item))
                throw new InvalidOperationException("O item não pertence a esta venda.");

            var canceledItem = new SaleItem(
                item.ProductId,
                item.Quantity,
                item.UnitPrice,
                item.IsEligibleForDiscount,
                isCanceled: true,
                () => Items.Count(i => !i.IsCanceled));

            int index = Items.IndexOf(item);
            Items[index] = canceledItem;
        }
    }
}
