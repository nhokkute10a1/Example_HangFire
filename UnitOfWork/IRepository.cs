using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LibCommon
{
    public interface IRepository<T>
    {
        //T FindById(params Expression<Func<T, object>>[] includeProperties);

        T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T FindFirst(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        //void RemoveByID(T id);

        void RemoveMultiple(List<T> entities);

        T FindByID(int id);

        void AddRange(List<T> range);
    }
}
