﻿using System.Data;
using System.Linq.Expressions;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Service
{
    public interface IServiceBase<T> where T : EntityBase<T> , new()
    {
        IQueryable<T> Load(); //== All method in repository
        
        T Create(T entity, bool allowSaveChange = true);
        void Create(List<T> objectList, bool allowSaveChange = true);
        int Delete(T entity, bool allowSaveChange = true);
        int Delete(Expression<Func<T, bool>> predicate, bool allowSaveChange = true);
        int Update(T entity, bool allowSaveChange = true);

        int Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatepredicate,
            bool allowSaveChange = true);

        int Count { get; }
        T Find(params object[] keys);
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate, bool allowFilterDeleted = true);
        IQueryable<T> Filter(string expression, bool allowFilterDeleted = true, params object[] value);
       // IQueryable<T> Filter(ExpressionInfo expressionInfo, out int total, bool allowFilterDeleted = true);

        IQueryable<T> Filter(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50);
        bool Contains(Expression<Func<T, bool>> predicate);
        T Find(Expression<Func<T, bool>> predicate, bool allowFilterDeleted = true);
        AppBase AppBase { get; }
       
    }
}
      

