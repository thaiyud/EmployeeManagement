namespace EmployeeManagement.Repositories
{
    public interface IRepository<T> where T : class    
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(string id);
        Task DeleteAsync(int id);
        Task<T> UpdateAsync(T entity);
    }
}
