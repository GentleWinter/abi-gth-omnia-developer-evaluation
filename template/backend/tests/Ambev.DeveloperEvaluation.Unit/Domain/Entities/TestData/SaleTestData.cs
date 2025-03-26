using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleTestData
    {

        public static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
            .RuleFor(i => i.ProductId, f => Guid.NewGuid())
            .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
            .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(1, 100))
            .RuleFor(i => i.IsCanceled, f => f.Random.Bool(0.2f));

        private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
            .RuleFor(s => s.SaleNumber, f => f.Random.Int(1000, 9999))
            .RuleFor(s => s.CreatedAt, f => f.Date.Past(2))
            .RuleFor(s => s.CustomerId, f => Guid.NewGuid())
            .RuleFor(s => s.BranchId, f => Guid.NewGuid())
            .RuleFor(s => s.IsCanceled, f => f.Random.Bool(0.1f))
            .RuleFor(s => s.Items, f => SaleItemFaker.Generate(f.Random.Int(1, 5)).ToList());

        public static Sale GenerateValidSale()
        {
            return SaleFaker.Generate();
        }

        public static int GenerateInvalidQuantity()
        {
            return new Faker().Random.Int(20, 30);
        }

        public static Faker<Sale> GenerateNotEligibleForDiscount()
        {
            Faker<Sale> sale = new Faker<Sale>()
                .RuleFor(s => s.SaleNumber, f => f.Random.Int(1000, 9999))
                .RuleFor(s => s.CreatedAt, f => f.Date.Past(2))
                .RuleFor(s => s.CustomerId, f => Guid.NewGuid())
                .RuleFor(s => s.BranchId, f => Guid.NewGuid())
                .RuleFor(s => s.IsCanceled, f => f.Random.Bool(0.1f))
                .RuleFor(s => s.Items, f => SaleItemFaker.Generate(f.Random.Int(1, 3)).ToList());

            return sale;
        }
    }
}
