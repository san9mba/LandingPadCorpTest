using Common;
using Infrastructure.Repositories;
using System;

namespace Core.Services.Users
{
    [AutoRegister]
    public class DeleteUserService : IDeleteUserService
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Delete(Guid id)
        {
            _userRepository.Delete(id);
        }

        public void DeleteAll()
        {
            _userRepository.DeleteAll();
        }
    }
}