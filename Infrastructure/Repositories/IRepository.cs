using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BusinessEntities;

namespace Infrastructure.Repositories
{
    //public interface IRepository<T> where T : IdObject
    //{
    //    void Save(T entity);
    //    void Delete(T entity);
    //    T Get(Guid id);
    //}
    public interface IRepository<T> where T : IdObject
    {
        T Get(Guid id);
        List<T> GetByExpression(int skip, int take, Expression<Func<T, bool>> filter);
        void Save(T entity);
        void Delete(Guid id);
        IEnumerable<T> GetAll();
    }
}