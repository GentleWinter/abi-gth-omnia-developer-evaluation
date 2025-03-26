namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Discount
    {
        public static bool IsEligibleForDiscount(int itemCount) => itemCount > 4;

        public static decimal CalculateDiscount(int quantity, decimal unitPrice, bool isEligible)
        {
            if (!isEligible) return 0m;

            if (quantity >= 10 && quantity <= 20)
                return (quantity * unitPrice) * 0.2m;

            if (quantity >= 4)
                return (quantity * unitPrice) * 0.1m;

            return 0m;
        }
    }
}
