using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Core.Ef;
using Core.Model;
using Core.Model.Interface;
using Core.Repository;
using System;
using System.Collections.Generic;
using Core.Repository.Interface;

namespace Core.Service
{
    public class ServiceBase
    {
        public static AppBase appBase;

        static ServiceBase()
        {
            appBase = new AppBase();
        }
    }

    public class ServiceBase<T> : ServiceBase, IServiceBase<T>, IDisposable where T : EntityBase<T>, new()
    {
        protected IDbContextBase ContextBase;
        protected IRepositoryBase<T> _repositoryBase;
        protected IUserLog UserLog;


        public ServiceBase(IDbContextBase refahContextBase)
        {
            ContextBase = refahContextBase;
            _repositoryBase = GetRepository();


        }

        public ServiceBase(IDbContextBase refahContextBase,IUserLog userLog)
        {
            ContextBase = refahContextBase;
            _repositoryBase = GetRepository();
            UserLog = userLog;
        }
        
      

        public IRepositoryBase<T> RepositoryBase
        {
            get { return _repositoryBase; }
        }


        public virtual IRepositoryBase<T> GetRepository()
        {
            return new RepositoryBase<T>(ContextBase);
        }

        public virtual IQueryable<T> Load()
        {
            return _repositoryBase.All();
        }

       


        public virtual T Create(T entity, bool allowSaveChange = true)
        {
            return _repositoryBase.Create(entity, allowSaveChange);
        }

        public virtual void Create(List<T> objectList, bool allowSaveChange = true)
        {
            _repositoryBase.Create(objectList, allowSaveChange);
        }

        public virtual int Delete(T entity, bool allowSaveChange = true)
        {

            return _repositoryBase.Delete(entity, allowSaveChange);

        }



        public virtual int Delete(Expression<Func<T, bool>> predicate, bool allowSaveChange = true)
        {
            return _repositoryBase.Delete(predicate, allowSaveChange);

        }
        public virtual int Update(T entity, bool allowSaveChange = true)
        {
            return _repositoryBase.Update(entity, allowSaveChange);
        }

        public virtual int Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatepredicate, bool allowSaveChange = true)
        {

            return _repositoryBase.Update(predicate, updatepredicate, allowSaveChange);

        }

        public virtual int Count { get { return _repositoryBase.Count; } }

        public virtual void Attach(dynamic entity)
        {
            _repositoryBase.Update(entity);

        }

        public virtual T Find(params object[] keys)
        {
            return _repositoryBase.Find(keys);
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> predicate, bool allowFilterDeleted = true)
        {
            return _repositoryBase.Filter(predicate);
        }

        public virtual IQueryable<T> Filter(string expression, bool allowFilterDeleted = true, params object[] value)
        {
            return _repositoryBase.Filter(expression,allowFilterDeleted, value);
        }

        //public IQueryable<T> Filter(ExpressionInfo expressionInfo, out int total, bool allowFilterDeleted = true)
        //{
        //    total = 0;
        //    return null;
        //    //return _repositoryBase.Filter(expressionInfo,total,);
        //}


        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50)
        {
            return _repositoryBase.Filter(filter, out total, index, size);
        }

        public virtual bool Contains(Expression<Func<T, bool>> predicate)
        {
            return _repositoryBase.Contains(predicate);
        }

        public virtual T Find(Expression<Func<T, bool>> predicate, bool allowFilterDeleted = true)
        {
            return _repositoryBase.Find(predicate);
        }



        public AppBase AppBase { get { return appBase; } }
       
        public virtual void Dispose()
        {
            if (_repositoryBase != null)
                _repositoryBase.Dispose();
        }


        //Remark:
        //public T CreateAndAttach(T TObject, Type attchObject, List<ob> attachedIdList)
        //{
        //    return _repositoryBase.CreateAndAttach(TObject, attchObject, attachedIdList);

        //}
    }
}
