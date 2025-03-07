using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Common.Exceptions;
using Data.Repositories;

namespace Core.Services.Users
{
    [AutoRegister]
    public class UpdateUserService : IUpdateUserService
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Update(User user, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags)
        {
            user.SetEmail(email);
            user.SetName(name);
            user.SetType(type);
            user.SetMonthlySalary(annualSalary ?? 0 / 12);
            user.SetTags(tags);
        }

        public User Update(Guid userId, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags)
        {
            var user = _userRepository.Get(userId) ?? throw new EntityNotFoundException(nameof(User), userId.ToString());
            Update(user, name, email, type, annualSalary, tags);
            return user;
        }
    }
}