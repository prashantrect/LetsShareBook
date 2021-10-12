
using LSB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSB.Repository
{
    public class UserRepository
    {
        public Task<User> GetUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(string userId, User user)
        {
            throw new NotImplementedException();
        }
    }
}
