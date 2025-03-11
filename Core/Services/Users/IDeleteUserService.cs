using BusinessEntities;
using System;

namespace Core.Services.Users
{
    public interface IDeleteUserService
    {
        void Delete(Guid id);
        void DeleteAll();
    }
}