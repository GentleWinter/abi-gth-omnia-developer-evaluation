using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    internal class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
    {
        public DeleteSaleValidator()
        {
            RuleFor(sale => sale.Id).NotNull().NotEmpty();
        }
    }
}
