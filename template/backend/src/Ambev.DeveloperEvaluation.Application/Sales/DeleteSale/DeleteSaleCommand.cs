﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
    {
        public DeleteSaleCommand(Guid id) 
        {
            Id = id;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
