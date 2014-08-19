using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq.Expressions;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model.Interface;

namespace Core.Ef
{
    public class DBSetBase<T> : IDbSetBase<T> where T : EntityBase<T>
    {
        private DbSet<T> _dbSet;
        public DBSetBase(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public ObservableCollection<T> Local
        {
            get { return _dbSet.Local; }
            private set { throw new NotImplementedException(); }
        }

        public T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public T Attach(T entity)
        {
            return _dbSet.Attach(entity);
        }

        public T Create()
        {
            return _dbSet.Create();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return _dbSet.Create<TDerivedEntity>();
        }

        public T Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public T Remove(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (_dbSet as IQueryable<T>).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_dbSet as IQueryable<T>).GetEnumerator();
        }

        public Expression Expression
        {
            get { return (_dbSet as IQueryable<T>).Expression; }
            private set { throw new NotImplementedException(); }
        }

        public Type ElementType
        {
            get { return (_dbSet as IQueryable<T>).ElementType; }
            private set { throw new NotImplementedException(); }
        }

        public IQueryProvider Provider
        {
            get { return (_dbSet as IQueryable<T>).Provider; }
            private set { throw new NotImplementedException(); }
        }
    }
}
