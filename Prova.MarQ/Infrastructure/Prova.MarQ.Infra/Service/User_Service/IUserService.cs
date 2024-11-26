using Prova.MarQ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Service.User_Service
{
    public interface IUserService
    {
        Task AddUser(User user);
        Task<User?> UserLogin(string userName, string password);
        public Task<User?> GetUserByName(string userName);

    }
}
