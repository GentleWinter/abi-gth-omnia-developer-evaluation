using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        [JsonConstructor]
        private SaleItem() { }

        public SaleItem(
            Guid productId,
            int quantity,
            decimal unitPrice,
            bool isEligibleForDiscount,
            bool isCanceled,
            Func<int> getNonCanceledItemsCount)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            IsEligibleForDiscount = isEligibleForDiscount;
            IsCanceled = isCanceled;
            _getNonCanceledItemsCount = getNonCanceledItemsCount;
            RecalculateAmounts();
        }

        [JsonIgnore]
        private readonly Func<int> _getNonCanceledItemsCount;

        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid ProductId { get; init; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; init; }
        public decimal DiscountAmount { get; private set; }
        public decimal TotalItemAmount { get; private set; }
        public bool IsCanceled { get; private set; }
        public bool IsEligibleForDiscount { get; private set; }

        public void RecalculateAmounts()
        {
            bool applyDiscount = _getNonCanceledItemsCount?.Invoke() >= 4 && IsEligibleForDiscount;
            DiscountAmount = applyDiscount ? Discount.CalculateDiscount(Quantity, UnitPrice, IsEligibleForDiscount) : 0;
            TotalItemAmount = (UnitPrice * Quantity) - DiscountAmount;
        }
        public void UpdateQuantity(int newQuantity)
        {
            Quantity = newQuantity;
            RecalculateAmounts();
        }
    }
}
