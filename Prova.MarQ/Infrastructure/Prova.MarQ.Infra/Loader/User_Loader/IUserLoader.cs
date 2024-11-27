using Microsoft.AspNetCore.Identity;
using Prova.MarQ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Loader.User_Loader
{
    public interface IUserLoader
    {
        Task AddUser(User user);
        Task<User?> GetUserByName(string userName);
        public string HashThePassword(User user);
        public PasswordVerificationResult VerifyHashedPassword(User user, string password);
    }
}
