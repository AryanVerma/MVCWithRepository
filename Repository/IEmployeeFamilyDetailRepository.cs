using MVCWithRepository.Models; 
using System.Collections.Generic; 
namespace MVCWithRepository.Repository
{
    interface IEmployeeFamilyDetailRepository
    {
        IEnumerable<EmployeeFamilyDetail> GetAll();
        EmployeeFamilyDetail GetById(int EmployeeID);
        void Insert(EmployeeFamilyDetail employee);
        void Update(EmployeeFamilyDetail employee);
        void Delete(int EmployeeID);
       // void Save();
    }
}
