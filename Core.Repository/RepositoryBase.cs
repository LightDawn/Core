using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using Core.Ef;
using Core.Model;
using Core.Model.Interface;
using Core.Repository.Interface;
using EntityFramework.Extensions;
using EntityState = Core.Model.Interface.EntityState;

namespace Core.Repository
{
    public class RepositoryBase<TObject> : IRepositoryBase<TObject> where TObject : EntityBase<TObject>, new()
    {
        protected IDbContextBase ContextBase;

        protected IUserLog UserLog;
        private IDbSetBase<TObject> dbSet;

        public RepositoryBase(IDbContextBase dbContextBase, IUserLog userLog)
        {
            ContextBase = dbContextBase;
            UserLog = userLog;

        }

        public RepositoryBase(IDbContextBase dbContextBase)
        {
            ContextBase = dbContextBase;

        }

        private static string _tableName;
        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(_tableName))
                {
                    ObjectContext objectContext = ((IObjectContextAdapter)ContextBase).ObjectContext;
                    Type entityType = typeof(TObject);

                    if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
                        entityType = entityType.BaseType;

                    string entityTypeName = entityType.Name;

                    EntityContainer container =
                        objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);
                    string entitySetName = (from meta in container.BaseEntitySets
                                            where meta.ElementType.Name == entityTypeName
                                            select meta.Name).First();
                    _tableName = entitySetName;
                }
                return _tableName;
            }
        }

        public static string _keyName;
        public string KeyName
        {
            get
            {
                if (string.IsNullOrEmpty(_keyName))
                {
                    ObjectContext objectContext = ((IObjectContextAdapter)ContextBase).ObjectContext;
                    var set = objectContext.CreateObjectSet<TObject>();
                    _keyName = set.EntitySet.ElementType
                                .KeyMembers
                                 .Select(k => k.Name).FirstOrDefault();


                    //EntityContainer container = objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);

                    //EntitySetBase entitySet = container.BaseEntitySets.Where(item => item.ElementType.Name.Equals(typeof(TObject).Name))
                    //                                                  .FirstOrDefault();

 //                   string className = typeof (TObject).Name;
 //var container =   
 //   objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);
 //   string setName = (from meta in container.BaseEntitySets
 //                                         where meta.ElementType.Name == className
 //                                         select meta.Name).First();


 //                   var ddd = objectContext.CreateEntityKey(setName,set).EntityKeyValues[0].Value;   

                }
                return _keyName;
            }
        }


        public static string _keyValue;
        public string KeyValue
        {
            get
            {
                if (string.IsNullOrEmpty(_keyValue))
                {
                    ObjectContext objectContext = ((IObjectContextAdapter)ContextBase).ObjectContext;
                    var set = objectContext.CreateObjectSet<TObject>();
                    //_keyName = set.EntitySet.ElementType
                    //            .KeyMembers
                    //             .Select(k => k.Name).FirstOrDefault();




                    ////string entitySet = GetEntitySetName<T>();
                    ////_context.AddObject(entitySet, entity);
                    ////_context.SaveChanges();

                    ////Returns primaryKey value
                    ////return (TR)
                    //var ddd = objectContext.CreateEntityKey(TableName, set).EntityKeyValues[0].Value;   

                   // var fff = ContextBase.Entry().GetDatabaseValues();
                  

                }
                return _keyValue;
            }
        }




       
        public
            IDbSetBase<TObject> DbSet
        {
            get
            {
                if (!(ContextBase is DbContext))
                {
                    dbSet = ContextBase.Set<TObject>();
                }
                else
                {
                    dbSet = new DBSetBase<TObject>((ContextBase as DbContext).Set<TObject>());
                }

                return dbSet;
            }
        }

        public void Dispose()
        {
            if (ContextBase != null)
                ContextBase.Dispose();
        }

        public virtual IQueryable<TObject> All()
        {
            return DbSet.AsQueryable();
        }

       

        public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate, bool allowFilterDeleted = true)
        {
            return DbSet.Where(predicate).AsQueryable<TObject>();
        }

        public virtual IQueryable<TObject> Filter(string expression, bool allowFilterDeleted = true, params object[] value)
        {
            return DbSet.Where(expression, value).AsQueryable();

        }


        public IQueryable<TObject> Filter(Expression<Func<TObject, bool>> filter,
         out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() :
                DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) :
                _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public IEnumerable<TObject> Filter(ExpressionInfo expressionInfo, out int total, bool allowFilterDeleted = true)
        {
            int skipCount = expressionInfo.CurrentPage * expressionInfo.PageSize;
            var _resetSet = expressionInfo.Expression !=  null ? DbSet.Where(((KeyValuePair<string,string>)expressionInfo.Expression).Key, expressionInfo.Expression.Value).AsQueryable() :
                DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(expressionInfo.PageSize) :
                _resetSet.Skip(skipCount).Take(expressionInfo.PageSize);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();

        }


        public bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public virtual TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);

        }

        public virtual TObject Find(Expression<Func<TObject, bool>> predicate, bool allowFilterDeleted = true)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual TObject Create(TObject t, bool allowSaveChange = true)
        {
           ContextBase.SetContextState(t, EntityState.Added);
            if (allowSaveChange)
                SaveChanges();
            return t;
        }

        public virtual void Create(List<TObject> objectList, bool allowSaveChange = true)
        {
            foreach (var item in objectList)
            {
                DbSet.Add(item);
            }

            if (allowSaveChange)
                SaveChanges();
        }

        public virtual int SaveChanges()
        {
            return ContextBase.SaveChanges();
        }



        public virtual int Delete(TObject t, bool allowSaveChange = true)
        {
            ContextBase.SetContextState(t, EntityState.Deleted);
            if (allowSaveChange)
                return SaveChanges();
            return 0;

        }



        public virtual int Count
        {
            get
            {
                return DbSet.Count();
            }
        }

        public virtual int Update(TObject t, bool allowSaveChange = true)
        {

            //example of using an IQueryable as the filter for the update
            //var users = context.Users.Where(u => u.FirstName == "firstname");
           // context.Users.Update(users, u => new User { FirstName = "newfirstname" });
            //CreateExpression(typeof(bool), _condition);
            // context.YourEntities.Local.Any(e => e.Id == id);
            //todo:...check in multiple user...
            //todo: ...Comment It because it doesn`t work in many to many ..(example in RoleOrganization Accout)
            //Func<TObject, bool> func = x => t[KeyName].ToString() == t[KeyName].ToString();
            //if (ContextBase.Set<TObject>().Local.Any(func))
            //{
            //    var foundObject = Find(t[KeyName]);
            //    var attachedEntry = ContextBase.Entry(foundObject);
            //    attachedEntry.CurrentValues.SetValues(t);
            //}

            //else
                ContextBase.SetContextState(t, EntityState.Modified);

            if (allowSaveChange)
                return SaveChanges();
            return 0;
        }



        //private LambdaExpression CreateExpression(Type objectType, string expression)
        //{
            
        //    LambdaExpression lambdaExpression =
        //        System.Linq.Dynamic.DynamicExpression.ParseLambda(
        //            objectType, typeof(bool), expression);
        //    return lambdaExpression;
        //}
    
        public virtual int Update(Expression<Func<TObject, bool>> predicate, Expression<Func<TObject, TObject>> updatepredicate, bool allowSaveChange = true)
        {
            
            DbSet.Update(predicate, updatepredicate);
            if (allowSaveChange)
                return SaveChanges();
            return 0;
        }


        public virtual int Delete(Expression<Func<TObject, bool>> predicate, bool allowSaveChange = true)
        {
            //Remark:Use Extended For Delete
            //var objects = Filter(predicate);
            //foreach (var obj in objects)
            //  DbSet.Remove(obj);
            DbSet.Delete(predicate);
            if (allowSaveChange)
                return SaveChanges();
            return 0;
        }

        //Remark:
        ////public virtual TObject CreateAndAttach<T>(TObject tObject, Type attachObject, List<object> attachedList, T t)
        ////{
        ////    //var role = new PegahRole() { Guid = pegahRole };

        ////    //agar in kar ra anjam dahim row jadid dar pegahrole inser nemikonad.
        ////    // ctx.PegahRoles.Attach(role);
        ////    // role.PegahUsers.Add(PegahUser);

        ////    var attacheDbset = ContextBase.Set(attachObject);
        ////    foreach (var item in attachedList)
        ////    {
        ////        var finded = attacheDbset.Find(item);
        ////        attacheDbset.Attach(finded);
        ////        (tObject["Roles"] as ICollection<T>).Add((T)finded);
        ////    }

        ////    var newEntry = DbSet.Add(tObject);
        ////    ContextBase.SaveChanges();
        ////    return newEntry;
        ////}

        ////public virtual TObject UpdateAndAttach<T>(TObject t, List<object> attachedList, List<T> removedList, Expression<Func<TObject, bool>> predicate)
        ////{
        ////    var attacheDbset = ContextBase.Set(typeof(T));

        ////    //  var removedList1= (t["Roles"] as ICollection<T>).ToList();


        ////    foreach (var item in attachedList)
        ////    {
        ////        var finded = attacheDbset.Find(item);
        ////        attacheDbset.Attach(finded);
        ////        (t["Roles"] as ICollection<T>).Add((T)finded);
        ////        // attacheDbset.PegahUsers.Add(PegahUser);
        ////    }

        ////    // (T["Users"] as t).Add(t);
        ////    DbSet.Add(t);

        ////    var users = DbSet.Include("Roles").Single(predicate);
        ////    var newRoles = attacheDbset.Where(r => selectedRoles.Contains(r.Id)).ToList();
        ////    foreach (var item in removedList)
        ////    {

        ////        (t["Roles"] as ICollection<T>).Clear();




        ////    }
        ////    ContextBase.SaveChanges();
        ////    return t;
        ////}
    }
}