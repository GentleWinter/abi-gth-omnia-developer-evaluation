using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(sale => sale).NotNull().NotEmpty();
            RuleFor(sale => sale.CustomerId).NotNull().NotEmpty();
            RuleFor(sale => sale.BranchId).NotNull().NotEmpty();
            RuleFor(sale => sale.Items.Count).GreaterThanOrEqualTo(1);
        }
    }
}
