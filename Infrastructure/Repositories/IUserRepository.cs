using System.Collections.Generic;
using BusinessEntities;

namespace Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> Get(int skip, int take, UserTypes? userType = null, string name = null, string email = null);
        IEnumerable<User> GetBytag(string tag);
        void DeleteAll();
    }
}