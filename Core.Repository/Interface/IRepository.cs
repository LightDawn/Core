using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Model;

namespace Core.Repository.Interface
{
   public interface IRepository<T>: IDisposable  where T : class
   {
       IQueryable<T> All();

       IQueryable<T> Filter(Expression<Func<T, bool>> predicate);

       IQueryable<T> Filter(string expression, params object[] value);

       IQueryable<T> Filter(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50);

       bool Contains(Expression<Func<T, bool>> predicate);

       T Find(params object[] keys);

       T Find(Expression<Func<T, bool>> predicate);

       T Create(T t,bool allowSaveChange=true);

       int Delete(T t, bool allowSaveChange=true);

       int Delete(Expression<Func<T, bool>> predicate, bool allowSaveChange = true);

       int Update(T t, bool allowSaveChange=true);

       int Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatepredicate, bool allowSaveChange = true);

       int Count { get; }

       int SaveChanges();
    
   }
}
