using Invoicing.Domain;

namespace Invoicing.Infrastructure
{
    public interface IGenericUnitOfWork
    {
        public Task CommitAsync();

        public void Commit();

        public void Dispose();

        public GenericRepository<Invoice> InvoiceRepository { get; }
    }
}
