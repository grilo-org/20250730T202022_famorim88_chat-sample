using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _context;
        public UnitOfWork(ChatDbContext context) => _context = context;
        public async Task CommitAsync() => await _context.SaveChangesAsync();
    }

}
