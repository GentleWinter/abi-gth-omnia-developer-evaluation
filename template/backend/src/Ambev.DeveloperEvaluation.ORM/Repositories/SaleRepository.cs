using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sale.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;

            _context.Sale.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sale.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Sale> UpdateAsync(Sale newSale, CancellationToken cancellationToken = default)
        {
            var oldSale = await GetByIdAsync(newSale.Id, cancellationToken);
            if(oldSale == null)
            {
                throw new KeyNotFoundException($"Sale with ID {newSale.Id} not found");
            }
            _context.Entry(oldSale).CurrentValues.SetValues(newSale);
            await _context.SaveChangesAsync(cancellationToken);
            return newSale;

        }
    }
}
