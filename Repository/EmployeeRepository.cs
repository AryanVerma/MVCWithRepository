using MVCWithRepository.Models;
using MVCWithRepository.Repository.GenericRepository;
using MVCWithRepository.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVCWithRepository.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        // private readonly EmployeeDbEntities _context;
        private GenericRepository<Employee> _repository = null;
        public EmployeeRepository(IUnitOfWork<EmployeeDbEntities> unitOfWork)
            : base(unitOfWork)
        {
            _repository = new GenericRepository<Employee>(unitOfWork);
        }
        public EmployeeRepository(EmployeeDbEntities context)
            : base(context)
        {
        }
        //public EmployeeRepository()
        //{
        //    //_context = new EmployeeDbEntities();
        //    _repository = new GenericRepository<Employee>();
        //}
        //public EmployeeRepository(IGenericRepository<Employee> repository)
        //{
        //    _repository = repository;
        //}
        //public EmployeeRepository(EmployeeDbEntities context)
        //{
        //    _context = context;
        //}
        public IEnumerable<Employee> GetAll()
        {
            //return _context.Employees.ToList();


            return _repository.Table.ToList();
        }
        public Employee GetById(int EmployeeID)
        {
            //return _context.Employees.Find(EmployeeID);
            return _repository.GetById(EmployeeID);
             
        }
        public void Insert(Employee employee)
        {
            employee.CreateDate = DateTime.Now;
            //_context.Employees.Add(employee);            
            _repository.Insert(employee);
            //_repository.Save();
        }
        public void Update(Employee employee)
        {
            employee.ModifiedDate = DateTime.Now;
            //_context.Entry(employee).State = EntityState.Modified;
            _repository.Update(employee);
            // _repository.Save();
        }
        public void Delete(int EmployeeID)
        {
            //Employee employee = _context.Employees.Find(EmployeeID);
            //_context.Employees.Remove(employee);

            //_repository.Delete(EmployeeID);
            Employee employee = _repository.GetById(EmployeeID);
            _repository.Delete(employee);
            //_repository.Save();
        }
        //public void Save()
        //{
        //    _context.SaveChanges();
        //}
        //private bool disposed = false;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}