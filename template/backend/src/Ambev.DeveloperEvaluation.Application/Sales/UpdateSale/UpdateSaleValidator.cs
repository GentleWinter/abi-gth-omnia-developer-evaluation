using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(sale => sale.SaleNumber).NotNull().NotEmpty();
            RuleFor(sale => sale.CustomerId).NotNull().NotEmpty();
            RuleFor(sale => sale.BranchId).NotNull().NotEmpty();
            RuleFor(sale => sale.Items.Count).GreaterThanOrEqualTo(1);
            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemValidator());
        }
    }
}
