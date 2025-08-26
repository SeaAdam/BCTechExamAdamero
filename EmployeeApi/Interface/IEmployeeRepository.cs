using BCTechExamAdamero.Models;

namespace BCTechExamAdamero.Interface
{
    public interface IEmployeeRepository 
    {
        Task<Employee> CreateAsync (Employee employeeModel);
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> FindByIdAsync (int id);
        Task<Employee?> UpdateAsync (int id, Employee employeeModel);
        Task<Employee?> DeleteAsync (int id);
    }
}
