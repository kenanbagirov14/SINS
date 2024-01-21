using NIS.DALCore.Context;
using NIS.DALCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIS.DALCore.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
        void BeginTransaction();
        int SaveChanges();
        //Task<int> SaveChangesAsync();
        void RollBack();
        void Commit();
        void Dispose();
    }
}
