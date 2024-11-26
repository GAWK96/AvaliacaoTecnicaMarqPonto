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

        public UserLoader(ProvaMarqDbContext context): base(context)
        {
            _context = context;
        }
        public async Task AddUser(User user)
        {
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
