using System;

namespace Sciencecom.Models.Patterns
{
    public class UnitOfWork : IDisposable
    {
        private SciencecomEntities context = new SciencecomEntities();
        //private GenericRepository<Department> departmentRepository;
        //private GenericRepository<Course> courseRepository;

        //public GenericRepository<Department> DepartmentRepository
        //{
        //    get
        //    {

        //        if (this.departmentRepository == null)
        //        {
        //            this.departmentRepository = new GenericRepository<Department>(context);
        //        }
        //        return departmentRepository;
        //    }
        //}


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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