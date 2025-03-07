using BusinessEntities;
using Common;
using Common.Exceptions;
using Data.Repositories;
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
            this.Delete(_userRepository.Get(id) ?? throw new EntityNotFoundException(nameof(User), id.ToString()));
        }
        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }

        public void DeleteAll()
        {
            _userRepository.DeleteAll();
        }
    }
}