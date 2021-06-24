using MVCWithRepository.Models;
using MVCWithRepository.Repository.GenericRepository;
using MVCWithRepository.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVCWithRepository.Repository
{
    public class EmployeeFamilyDetailRepository : GenericRepository<EmployeeFamilyDetail>, IEmployeeFamilyDetailRepository
    {
         
        private IGenericRepository<EmployeeFamilyDetail> _repository = null;
         
        public EmployeeFamilyDetailRepository(IUnitOfWork<EmployeeDbEntities> unitOfWork)
            : base(unitOfWork)
        {
            _repository = new GenericRepository<EmployeeFamilyDetail>(unitOfWork);
        }
        public EmployeeFamilyDetailRepository(EmployeeDbEntities context)
            : base(context)
        {
        }
        //public EmployeeFamilyDetailRepository()
        //{

        //    _repository = new GenericRepository<EmployeeFamilyDetail>();
        //}
        //public EmployeeFamilyDetailRepository(IGenericRepository<EmployeeFamilyDetail> repository)
        //{
        //    _repository = repository;
        //}

        public IEnumerable<EmployeeFamilyDetail> GetAll()
        {
          
            return _repository.GetAll();
        }
        public EmployeeFamilyDetail GetById(int EmployeeID)
        {
           
            return _repository.GetById(EmployeeID);
        }


        public void Insert(EmployeeFamilyDetail employee)
        {

            _repository.Insert(employee);
           // _repository.Save();
        }
        public void Update(EmployeeFamilyDetail employee)
        {

            _repository.Update(employee);
            //_repository.Save();
        }
        public void Delete(int EmployeeID)
        {

            //_repository.Delete(EmployeeID);

            _repository.Delete(_repository.GetById(EmployeeID));
           // _repository.Save();
        }

    }
}