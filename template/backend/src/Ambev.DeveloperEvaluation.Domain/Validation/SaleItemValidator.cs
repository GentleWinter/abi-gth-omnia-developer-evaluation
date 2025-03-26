using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator() 
        { 
            RuleFor(saleItem => saleItem.Quantity).LessThanOrEqualTo(20).WithMessage("You canot add more than 20 items per product");
        }
    }
}
