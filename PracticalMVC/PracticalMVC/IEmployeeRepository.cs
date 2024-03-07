using PracticalMVC.Models;

namespace PracticalMVC
{
    public interface IEmployeeRepository
    {
        List<Employees_View> GetAllEmployees();
        Employee GetEmployeeById(int id);
        void InsertOrUpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
}
