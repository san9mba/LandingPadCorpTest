using BusinessEntities;
using Common;
using Data.DataProviders;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Providers
{
    [AutoRegister]
    public class RavenDbDataProviedr<T> : IRavenDbDataProvider<T>, IDataProvider<T> where T : IdObject
    {
        private readonly IDocumentSession _documentSession;

        public RavenDbDataProviedr(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public IDocumentSession DocumentSession => _documentSession;

        public T GetById(Guid id)
        {
            return _documentSession.Load<T>(id);
        }

        public void Save(T entity)
        {
            _documentSession.Store(entity);
        }

        public void Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity != null)
                _documentSession.Delete(entity);
        }

        public void Delete(T entity)
        {
            if (entity != null)
                _documentSession.Delete(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _documentSession.Query<T>().ToList();
        }

        public void DeleteAll<TIndex>() where TIndex : AbstractIndexCreationTask<T>
        {
            _documentSession.Advanced.DocumentStore.DatabaseCommands.DeleteByIndex(typeof(TIndex).Name, new IndexQuery());
        }

        public IQueryable<T> Query()
        {
            return _documentSession.Query<T>();
        }
    }
}
