using PostingControlService.Domain.Entities;
using System.Threading.Tasks;

namespace PostingControlService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string username, string password);
        Task<bool> AddUser(User user);
    }
}