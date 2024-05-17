namespace EmployeeManagement.Services.Interfaces
{
    public interface ISalaryService
    {
        Task<decimal> CheckSalary(string userId, int month, int year);

        //Task<decimal> CalculateSalary(string userId, int month, int year, decimal allowances, decimal bonuses, decimal deductions);
    
    }
}
