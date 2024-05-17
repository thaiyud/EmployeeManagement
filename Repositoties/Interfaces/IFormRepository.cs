using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Repositories
{
    public interface IFormRepository : IRepository<Form>
    {
        Task<IEnumerable<Form>> GetFormsByUserEmailAsync(string email);
        Task<List<Form>> GetFormsByUserIdAndMonthAsync(string userId, int month, int year);
    }
}
