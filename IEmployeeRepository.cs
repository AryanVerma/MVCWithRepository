using MVCWithRepository.Models; 
using System.Collections.Generic; 
namespace MVCWithRepository.Repository
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int EmployeeID);
        void Insert(Employee employee);
        void Update(Employee employee);
        void Delete(int EmployeeID);
        void Save();
    }
}
