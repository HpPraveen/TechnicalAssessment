using Invoicing.Domain;
using static System.GC;

namespace Invoicing.Infrastructure
{
    public class GenericUnitOfWork : IGenericUnitOfWork
    {
        private readonly CosmosDbContext _context;
        private bool _disposed;

        public GenericUnitOfWork(CosmosDbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            SuppressFinalize(this);
        }

        public GenericRepository<Invoice> InvoiceRepository => new(_context);
    }
}
