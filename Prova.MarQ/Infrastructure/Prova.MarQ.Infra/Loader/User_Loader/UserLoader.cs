using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Prova.MarQ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Loader.User_Loader
{
    public class UserLoader : BaseLoader, IUserLoader
    {
        private readonly ProvaMarqDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserLoader(ProvaMarqDbContext context): base(context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public string HashThePassword(User user)
        {
            var hashedPassword =  _passwordHasher.HashPassword(user, user.Password);
            user.Password = hashedPassword;
            return hashedPassword;
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string password)
        {
            //var hashedPassword = HashThePassword(user);
            var result =  _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return result;
        }
        public async Task AddUser(User user)
        {
            HashThePassword(user);
            await _context.User.AddAsync(user);
            await SaveChangesBusiness();
        }

        public async Task<User?> GetUserByName(string userName)
        {
            var getUserName = await _context.User
                                .Where(x => x.UserName == userName)
                                .FirstOrDefaultAsync();
            return getUserName;
        }
    }
}
