using Data.DataAccess;
using Data.DataAccess.Repositories;
using Data.EF.DataAccess.Repositories;

namespace Data.EF.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext context;

        public UnitOfWork(DatabaseContext context)
        {
            this.context = context;
            Customers = new CustomerRepository(context);
        }

        public ICustomerRepository Customers { get; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
