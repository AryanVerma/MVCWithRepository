using MVCWithRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVCWithRepository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbEntities _context;
        public EmployeeRepository()
        {
            _context = new EmployeeDbEntities();
        }
        public EmployeeRepository(EmployeeDbEntities context)
        {
            _context = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
        public Employee GetById(int EmployeeID)
        {
            return _context.Employees.Find(EmployeeID);
        }
        public void Insert(Employee employee)
        {
            employee.CreateDate = DateTime.Now;
            _context.Employees.Add(employee);
        }
        public void Update(Employee employee)
        {
            employee.ModifiedDate = DateTime.Now;
            _context.Entry(employee).State = EntityState.Modified;
        }
        public void Delete(int EmployeeID)
        {
            Employee employee = _context.Employees.Find(EmployeeID);
            _context.Employees.Remove(employee);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}