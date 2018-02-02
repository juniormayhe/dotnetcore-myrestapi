using RestTestWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestTestWebApp.Services
{
    public class UserService : IUserService
    {
        public Task<bool> Register(UserModel userModel) {
            return Task.FromResult(true);
        }

        public Task<bool> Login(UserModel userModel)
        {
            return Task.FromResult(true);
        }
    }
}
