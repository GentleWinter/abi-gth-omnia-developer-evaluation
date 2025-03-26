using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public UpdateSaleHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToString());
            }

            var existingSale = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (existingSale == null)
            {
                throw new InvalidOperationException($"Sale not found with id {request.Id}");
            }

            var updatedSale = _mapper.Map<Sale>(request);

            if (request.IsCanceled)
            {
                if (existingSale.IsCanceled)
                {
                    throw new InvalidOperationException("Sale is already canceled");
                }

                existingSale.CancelSale();
            }
            else if (request.Items != null && request.Items.Any())
            {
                if (existingSale.IsCanceled)
                {
                    throw new InvalidOperationException("Cannot add items to a canceled sale");
                }

                foreach (var newItem in updatedSale.Items)
                {
                    if (newItem.ProductId == Guid.Empty)
                    {
                        throw new ArgumentException("ProductId cannot be empty");
                    }

                    var existingItem = existingSale.Items.FirstOrDefault(i => i.ProductId == newItem.ProductId);

                    if (existingItem != null)
                    {
                        var updatedItem = new SaleItem(
                            existingItem.ProductId,
                            existingItem.Quantity + newItem.Quantity,
                            existingItem.UnitPrice,
                            existingSale.IsEligibleForDiscount,
                            existingItem.IsCanceled,
                            () => existingSale.Items.Count(i => !i.IsCanceled));

                        existingSale.RemoveItem(existingItem);
                        existingSale.AddItem(updatedItem);
                    }
                    else
                    {
                        existingSale.AddItem(newItem);
                    }
                }
            }

            var result = await _repository.UpdateAsync(existingSale, cancellationToken);
            return _mapper.Map<UpdateSaleResult>(result);
        }
    }
}
