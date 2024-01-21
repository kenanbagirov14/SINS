using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NIS.DALCore.Repository
{
    public interface IRepository<T>
    {
        #region IRepository Get

        IQueryable<T> GetAll(params string[] includes);

        T GetById(int id, params string[] includes);

        #endregion

        #region IRepository Find

        T Find(Expression<Func<T, bool>> predicate, params string[] includes);

        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params string[] includes);

        #endregion

        #region IRepository Add

        T Add(T entity, params string[] includes);

        List<T> AddRange(List<T> entities, params string[] includes);

        #endregion

        #region IRepository Update

        T Update(T entity);
        List<T> UpdateRange(List<T> entity);

        #endregion

        #region IRepository Remove

        void Remove(T entity);

        void RemoveRange(List<T> entities);
        #endregion
    }
}
