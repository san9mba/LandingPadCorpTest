using BusinessEntities;
using Raven.Client.Indexes;
using System.Linq;

namespace Data.Indexes
{
    public class UsersListByTagIndex : AbstractIndexCreationTask<User>
    {
        public UsersListByTagIndex()
        {
            Map = users => from user in users
                           select new
                           {
                               user.Tags,
                               Tags_Count = user.Tags.Count()
                           };
        }
    }
}
