using FoundersDesk.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoundersDesk.Interfaces
{
    public interface IVideoRepository
    {
        Task<List<Video>> GetVideosByModuleAsync(int moduleId);
    }
}
