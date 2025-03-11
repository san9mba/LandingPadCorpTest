using BusinessEntities;
using BusinessEntities.Sales;
using Common;
using Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFDataProvider
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class InMemoryProvider<T> : IDataProvider<T> where T : IdObject
    {
        private Dictionary<Guid, Product> _products { get; }
        private Dictionary<Guid, Order> _orders { get; }

        private Dictionary<Guid, T> _storage
        {
            get
            {
                if (typeof(T) == typeof(Product))
                    return (Dictionary<Guid, T>)(object)_products;
                else if (typeof(T) == typeof(Order))
                    return (Dictionary<Guid, T>)(object)_orders;
                else
                    throw new InvalidOperationException("Unsupported entity type.");
            }
        }

        public InMemoryProvider()
        {
            _products = new Dictionary<Guid, Product>();
            _orders = new Dictionary<Guid, Order>();
        }

        public T GetById(Guid id)
        {
            _storage.TryGetValue(id, out var entity);
            return entity;
        }

        public void Save(T entity)
        {
            var key = (Guid)typeof(T).GetProperty("Id").GetValue(entity);
            if (_storage.ContainsKey(key))
                _storage[key] = entity;
            else
                _storage.Add(key, entity);
        }

        public void Delete(Guid id)
        {
            _storage.Remove(id);
        }

        public void Delete(T entity)
        {
            if (entity != null)
                _storage.Remove(entity.Id);
        }

        public IEnumerable<T> GetAll()
        {
            return _storage.Values;
        }

        public IQueryable<T> Query()
        {
            return _storage.Values.AsQueryable();
        }
    }
}
