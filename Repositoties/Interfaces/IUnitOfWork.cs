using EmployeeManagement.Repositories;

namespace EmployeeManagement.repositoties.interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        IFormRepository Forms { get; }
        Task<int> SaveChangesAsync();
    }
}
