using System;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Dal.Repositories.EntityFramework
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MyUniversityDbContext myUniversityDbContext;
        private bool disposed = false;
        public UnitOfWork(MyUniversityDbContext dbContext)
        {
            myUniversityDbContext = dbContext;
        }

        public int Commit()
        {
            return myUniversityDbContext.SaveChanges();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    myUniversityDbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
