using MVCWithRepository.Models;
using MVCWithRepository.Repository.UnitOfWork; 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;


namespace MVCWithRepository.Repository.GenericRepository
{
    // public class GenericRepository<T> : IGenericRepository<T> where T : class
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        //private EmployeeDbEntities _context = null;
        //private DbSet<T> table = null;
       
        private IDbSet<T> _entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;
        public GenericRepository(IUnitOfWork<EmployeeDbEntities> unitOfWork)
            : this(unitOfWork.Context)
        {
        }
        public GenericRepository(EmployeeDbEntities context)
        {
            _isDisposed = false;
            Context = context;
        }
        public EmployeeDbEntities Context { get; set; }
        //public GenericRepository()
        //{
        //    this._context = new EmployeeDbEntities();
        //    table = _context.Set<T>();
        //}
        //public GenericRepository(EmployeeDbEntities _context)
        //{
        //    this._context = _context;
        //    table = _context.Set<T>();
        //}
         
         

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = Context.Set<T>();
                }
                return _entities;
            }
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }
        public IEnumerable<T> GetAll()
        {
            // return table.ToList();
            return _entities.ToList();
        }
        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                Entities.Add(entity);
                if (Context == null || _isDisposed)
                    Context = new EmployeeDbEntities();
                Context.SaveChanges(); //commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public void BulkInsert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }
                Context.Configuration.AutoDetectChangesEnabled = false;
                Context.Set<T>().AddRange(entities);
                Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName,
                                             validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    Context = new EmployeeDbEntities();
                SetEntryModified(entity);
                Context.SaveChanges(); //commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    Context = new EmployeeDbEntities();
                Entities.Remove(entity);
                Context.SaveChanges(); //commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void SetEntryModified(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
        //public T GetById(object id)
        //{
        //    return table.Find(id);

        //}
        //public void Insert(T obj)
        //{
        //    table.Add(obj);
        //}
        //public void Update(T obj)
        //{
        //    table.Attach(obj);
        //    _context.Entry(obj).State = EntityState.Modified;
        //}
        //public void Delete(object id)
        //{
        //    T existing = table.Find(id);
        //    table.Remove(existing);
        //}
        //public void Save()
        //{
        //    _context.SaveChanges();
        //}
    }
}