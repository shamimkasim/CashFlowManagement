using System.Threading.Tasks;

namespace PostingControlService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string username, string password);
        Task<bool> Register(string username, string password);
    }
}