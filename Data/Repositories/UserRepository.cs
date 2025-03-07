using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IDocumentSession _documentSession;

        public UserRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<User> Get(int skip, int take, UserTypes? userType = null, string name = null, string email = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<User, UsersListIndex>();

            var hasFirstParameter = false;
            if (userType != null)
            {
                query = query.WhereEquals("Type", (int)userType);
                hasFirstParameter = true;
            }

            if (name != null)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.Where($"Name:*{name}*");
            }

            if (email != null)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.WhereEquals("Email", email);
            }

            return query
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public IEnumerable<User> GetBytag(string tag)
        {
            var query = _documentSession.Advanced.DocumentQuery<User, UsersListByTagIndex>();
            if (string.IsNullOrEmpty(tag))
                // I'm not expert at Raven and not sure how effective this search it, but you can retreive users without tags
                query = query.Where("Tags_Count:0");
            else
                query = query.WhereIn(nameof(User.Tags), new[] { tag });
            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<UsersListIndex>();
        }
    }
}