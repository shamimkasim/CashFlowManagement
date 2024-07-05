using Microsoft.EntityFrameworkCore;
using PostingControlService.Domain.Entities;
using PostingControlService.Domain.Interfaces;
using PostingControlService.Infrastructure.Data;
using System.Threading.Tasks;

namespace PostingControlService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TransactionContext _context;

        public UserRepository(TransactionContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<bool> AddUser(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}