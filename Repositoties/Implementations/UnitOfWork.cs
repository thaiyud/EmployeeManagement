using EmployeeManagement.Data;
using EmployeeManagement.Repositories;
using EmployeeManagement.repositoties.interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EmployeeManagement.Repositoties.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EmployeeManagementDBContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(EmployeeManagementDBContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
          
            return _serviceProvider.GetService<IRepository<T>>() ?? throw new InvalidOperationException("Repository not found");
        }

        public IFormRepository Forms => _serviceProvider.GetService<IFormRepository>() ?? throw new InvalidOperationException("FormRepository not found");

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
