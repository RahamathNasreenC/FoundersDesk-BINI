using FoundersDesk.DTOs;
using FoundersDesk.Models;
using System.Threading.Tasks;

namespace FoundersDesk.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(RegisterRequestDto registerDto);
        Task<bool> UserExistsAsync(string username, string email);
        Task<Session> CreateSessionAsync(int userId);
        Task<bool> ValidateSessionAsync(string sessionToken);
        Task InvalidateSessionAsync(string sessionToken);
    }
}
