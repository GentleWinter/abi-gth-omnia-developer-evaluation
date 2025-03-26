using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid) 
            {
                throw new ValidationException(validationResult.Errors.ToString());
            }

            var sale = _mapper.Map<Sale>(request);

            var createdSale = await _repository.CreateAsync(sale, cancellationToken);
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            
            return result;
        }
    }
}
