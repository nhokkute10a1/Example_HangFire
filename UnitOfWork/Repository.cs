using Microsoft.EntityFrameworkCore;
using RouterDelivery.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LibCommon
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RouteDeliveryContext _context;
        public readonly DbSet<T> _dbSet;

        public Repository(RouteDeliveryContext context)
        {
            _context = context;

            _dbSet = _context.Set<T>();
        }

        public T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties).AsNoTracking().SingleOrDefault(predicate);
        }
        public T FindFirst(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties).AsNoTracking().FirstOrDefault(predicate);
        }

        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate);
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveMultiple(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public T FindByID(int id)
        {
            return _dbSet.Find(id);
        }

        public void AddRange(List<T> range)
        {
             _dbSet.AddRange(range);
        }



        //public void Dispose()
        //{
        //    if (_context != null)
        //    {
        //        _context.Dispose();
        //    }
        //}



        ////public T FindById(KeyExtensions id,params Expression<Func<T, object>>[] includeProperties)
        ////{
        ////    return FindAll(includeProperties).AsNoTracking().SingleOrDefault(x => x.Id.Equals(id));
        ////}

        ////public void Remove(K id)
        ////{
        ////    var entity = FindById(id);
        ////    Remove(entity);
        ////}

    }
}
