using MVCWithRepository.Models;
using MVCWithRepository.Repository;
using MVCWithRepository.Repository.GenericRepository;
using MVCWithRepository.Repository.UnitOfWork;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MVCWithRepository.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private IEmployeeFamilyDetailRepository _employeefamilyDetailRepository;

        private UnitOfWork<EmployeeDbEntities> unitOfWork = new UnitOfWork<EmployeeDbEntities>();  
       
        public EmployeeController()
        {
            // _employeeRepository = new EmployeeRepository();
            //If you want to use Generic Repository with Unit of work  
            _employeefamilyDetailRepository = new EmployeeFamilyDetailRepository(unitOfWork);
            _employeeRepository = new EmployeeRepository(unitOfWork);  
        }



        [HttpGet]
        public ActionResult Index()
        {

            var model = _employeeRepository.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                //_employeeRepository.Insert(model);
                //_employeefamilyRepository.Insert(new EmployeeFamilyDetail() { EmployeeId = model.Id, Name = model.Name });   


                try
                {
                    unitOfWork.CreateTransaction();
                    if (ModelState.IsValid)
                    {
                        _employeeRepository.Insert(model);
                        _employeefamilyDetailRepository.Insert(new EmployeeFamilyDetail() { EmployeeId = model.Id, Name = model.Name });
                        //Do Some Other Task with the Database
                        unitOfWork.Save();
                        //If everything is working then commit the transaction else rollback the transaction
                        unitOfWork.Commit();
                        return RedirectToAction("Index", "Employee");
                    }
                }
                catch (Exception ex)
                {
                    //Log the exception and rollback the transaction
                    unitOfWork.Rollback();
                }

                return RedirectToAction("Index", "Employee");

                // _employeeRepository.Save();
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditEmployee(int EmployeeId)
        {
            Employee model = _employeeRepository.GetById(EmployeeId);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Update(model);
                unitOfWork.Save();

                //  _employeeRepository.Save();
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult DeleteEmployee(int EmployeeId)
        {
            Employee model = _employeeRepository.GetById(EmployeeId);
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            _employeeRepository.Delete(Id);
           // _employeeRepository.Save();
            return RedirectToAction("Index", "Employee");
        }
    }
}