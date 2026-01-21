using FoundersDesk.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoundersDesk.Interfaces
{
    public interface IVideoRepository
    {
        Task<List<Video>> GetAllVideosAsync();
        Task<List<Video>> GetVideosByRoleAsync(string roleType);
        Task<List<Video>> GetVideosByCategoryAsync(string category);
        Task<List<Video>> GetVideosByRoleAndCategoryAsync(string roleType, string category);
        Task<Video> GetVideoByIdAsync(int videoId);
        Task<Video> CreateVideoAsync(Video video);
        Task<Video> UpdateVideoAsync(Video video);
        Task<bool> DeleteVideoAsync(int videoId);
    }
}
