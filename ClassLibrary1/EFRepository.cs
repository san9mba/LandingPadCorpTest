using BusinessEntities;
using Common;
using Infrastructure.Repositories;
using System;

namespace EFDataProvider
{
    [AutoRegister]
    internal class EFRepository<T> : IRepository<T> where T : IdObject
    {
        void IRepository<T>.Delete(T entity)
        {
            throw new NotImplementedException();
        }

        T IRepository<T>.Get(Guid id)
        {
            throw new NotImplementedException();
        }

        void IRepository<T>.Save(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
