using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Users
{
    public interface IGetUserService
    {
        User GetUser(Guid id);
        IEnumerable<User> GetUsers(int skip, int take, UserTypes? userType = null, string name = null, string email = null);
        IEnumerable<User> GetUsersByTag(string tag);
    }
}