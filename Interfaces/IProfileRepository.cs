using FoundersDesk.Models;
using System.Threading.Tasks;

namespace FoundersDesk.Interfaces
{
    public interface IProfileRepository
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task UpdateProfileAsync(User user);
    }
}
