using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator() 
        {
            RuleFor(sale => sale).NotNull().NotEmpty();
            RuleFor(sale => sale.CustomerId).NotNull().NotEmpty();
            RuleFor(sale => sale.BranchId).NotNull().NotEmpty();
            RuleFor(sale => sale.Items.Count).GreaterThanOrEqualTo(1);
        }
    }
}
