using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTest
    {
        [Fact]
        public void GenerateValidSale_ShouldReturnValidSale()
        {
            // Arrange & Act
            var sale = SaleTestData.GenerateValidSale();

            // Assert
            Assert.NotNull(sale);
            Assert.InRange(sale.SaleNumber, 1000, 9999);
            Assert.NotEqual(default(DateTime), sale.CreatedAt);
            Assert.NotEqual(Guid.Empty, sale.CustomerId);
            Assert.NotEqual(Guid.Empty, sale.BranchId);
            Assert.NotNull(sale.Items);
            Assert.InRange(sale.Items.Count, 1, 5);
        }

        [Fact]
        public void TotalSaleAmount_ShouldCalculateCorrectly()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var expectedTotal = sale.Items
                .Where(i => !i.IsCanceled)
                .Sum(i => i.UnitPrice * i.Quantity);

            // Assert
            Assert.Equal(expectedTotal, sale.TotalSaleAmount);
        }

        [Fact]
        public void IsEligibleForDiscount_WhenMoreThan5NonCanceledItems_ShouldReturnTrue()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            foreach (var item in sale.Items)
            {
                sale.RemoveItem(item);
            }

            // Adiciona 6 itens não cancelados
            for (int i = 0; i < 6; i++)
            {
                sale.AddItem(SaleTestData.SaleItemFaker.Generate());
            }

            // Act & Assert
            Assert.True(sale.IsEligibleForDiscount);
        }

        [Fact]
        public void GenerateNotEligibleForDiscount_ShouldReturnSaleWithLessThan5Items()
        {
            // Arrange
            var saleFaker = SaleTestData.GenerateNotEligibleForDiscount();
            var sale = saleFaker.Generate();

            // Act
            var nonCanceledItemsCount = sale.Items.Count(i => !i.IsCanceled);

            // Assert
            Assert.True(nonCanceledItemsCount < 5, "A venda deve ter menos de 5 itens não cancelados");
        }

        [Fact]
        public void IsCanceled_ShouldNotAffectTotalSaleAmount()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var canceledItem = sale.Items.First();
            sale.CancelSaleItem(canceledItem);

            // Act
            var expectedTotal = sale.Items
                .Where(i => !i.IsCanceled)
                .Sum(i => i.UnitPrice * i.Quantity);

            // Assert
            Assert.Equal(expectedTotal, sale.TotalSaleAmount);
            Assert.DoesNotContain(canceledItem, sale.Items.Where(i => !i.IsCanceled));
        }

        [Fact]
        public void GenerateInvalidQuantity_ShouldReturnValueAboveValidRange()
        {
            // Arrange & Act
            var invalidQuantity = SaleTestData.GenerateInvalidQuantity();

            // Assert
            Assert.InRange(invalidQuantity, 20, 30);
        }
    }
}
