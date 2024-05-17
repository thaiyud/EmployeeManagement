using EmployeeManagement.Models;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Repositoties.Interfaces
{
    public interface ISalaryRepository : IRepository<MonthlySalary>
    {
        Task<MonthlySalary> GetMonthlySalaryByUserIdAsync(string userId, int month, int year);
        Task<BasicSalary> GetBasicSalaryByUserIdAsync(string userId);
    }
}
