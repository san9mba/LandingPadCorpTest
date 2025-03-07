﻿using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Users
{
    [AutoRegister]
    public class GetUserService : IGetUserService
    {
        private readonly IUserRepository _userRepository;

        public GetUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(Guid id)
        {
            return _userRepository.Get(id);
        }

        public IEnumerable<User> GetUsers(int skip, int take, UserTypes? userType = null, string name = null, string email = null)
        {
            return _userRepository.Get(skip, take, userType, name, email);
        }
        public IEnumerable<User> GetUsersByTag(string tag)
        {
            return _userRepository.GetBytag(tag);
        }
    }
}