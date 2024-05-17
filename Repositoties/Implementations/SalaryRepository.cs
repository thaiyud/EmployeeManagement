using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using EmployeeManagement.Repositoties.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositoties.Implementations
{

    public class SalaryRepository : Repository<MonthlySalary>, ISalaryRepository
    {
        private readonly EmployeeManagementDBContext _context;
        public SalaryRepository(EmployeeManagementDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MonthlySalary> GetMonthlySalaryByUserIdAsync(string userId, int month, int year)
        {
            return await _context.MonthlySalaries
                .Include(ms => ms.BasicSalary)
                .FirstOrDefaultAsync(ms => ms.BasicSalary.UserId == userId && ms.Month == month && ms.Year == year);
        }

        public async Task<BasicSalary> GetBasicSalaryByUserIdAsync(string userId)
        {
            return await _context.BasicSalaries.FirstOrDefaultAsync(bs => bs.UserId == userId);
        }

    }
}
