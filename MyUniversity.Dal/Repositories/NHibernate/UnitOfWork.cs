using System;
using MyUniversity.Dal.Repositories.Contracts;
using NHibernate;

namespace MyUniversity.Dal.Repositories.NHibernate
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITransaction transaction;
        private readonly ISession session;

        public UnitOfWork(ISession session)
        {
            this.session = session;
            this.session.FlushMode = FlushMode.Auto;
            transaction = this.session.BeginTransaction();
        }

        public void Dispose()
        {
            if (session.IsOpen)
            {
                session.Close();
            }
        }

        public int Commit()
        {
            return ComitTransaction();
        }

        private int ComitTransaction()
        {
            if (!transaction.IsActive)
            {
                transaction = session.BeginTransaction();
            }

            try
            {
                transaction.Commit();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}