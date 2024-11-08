using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Loader
{
    public class BaseLoader
    {
        private readonly ProvaMarqDbContext _context;
        public BaseLoader(ProvaMarqDbContext context)
        {
            _context = context;
        }
        public async Task SaveChangesBusiness()
        {
            await _context.SaveChangesAsync();
        }
    }
}
