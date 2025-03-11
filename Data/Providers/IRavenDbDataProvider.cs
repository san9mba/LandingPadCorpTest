using BusinessEntities;
using Data.DataProviders;
using Raven.Client;
using Raven.Client.Indexes;

namespace Data.Providers
{
    public interface IRavenDbDataProvider<T> : IDataProvider<T> where T : IdObject
    {
        IDocumentSession DocumentSession { get; }
        void DeleteAll<TIndex>() where TIndex : AbstractIndexCreationTask<T>;
    }
}
