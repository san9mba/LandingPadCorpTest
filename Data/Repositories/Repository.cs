using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessEntities;
using Infrastructure.DataProviders;
using Infrastructure.Repositories;

namespace Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : IdObject
    {
        protected readonly IDataProvider<T> _dataProvider;

        protected Repository(IDataProvider<T> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public virtual T Get(Guid id) => _dataProvider.GetById(id);
        public List<T> GetByExpression(int skip, int take, Expression<Func<T, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException("Filter expression is null");

            return _dataProvider.Query()
                .Where(filter)
                .Skip(skip)
                .Take(take)
                .ToList();
        }
        public virtual void Save(T entity) => _dataProvider.Save(entity);
        public virtual void Delete(Guid id) => _dataProvider.Delete(id);
        public virtual void Delete(T entity) => _dataProvider.Delete(entity);
        public virtual IEnumerable<T> GetAll() => _dataProvider.GetAll();
    }
}