using FoundersDesk.Data;
using FoundersDesk.Interfaces;
using FoundersDesk.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersDesk.Data;

namespace FoundersDesk.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Video>> GetAllVideosAsync()
        {
            return await _context.Videos
                .Where(v => v.IsActive)
                .OrderBy(v => v.DisplayOrder)
                .ToListAsync();
        }

        public async Task<List<Video>> GetVideosByRoleAsync(string roleType)
        {
            return await _context.Videos
                .Where(v => v.IsActive && v.RoleType == roleType)
                .OrderBy(v => v.DisplayOrder)
                .ToListAsync();
        }

        public async Task<List<Video>> GetVideosByCategoryAsync(string category)
        {
            return await _context.Videos
                .Where(v => v.IsActive && v.Category == category)
                .OrderBy(v => v.DisplayOrder)
                .ToListAsync();
        }

        public async Task<List<Video>> GetVideosByRoleAndCategoryAsync(string roleType, string category)
        {
            return await _context.Videos
                .Where(v => v.IsActive && v.RoleType == roleType && v.Category == category)
                .OrderBy(v => v.DisplayOrder)
                .ToListAsync();
        }

        public async Task<Video> GetVideoByIdAsync(int videoId)
        {
            return await _context.Videos.FindAsync(videoId);
        }

        public async Task<Video> CreateVideoAsync(Video video)
        {
            _context.Videos.Add(video);
            await _context.SaveChangesAsync();
            return video;
        }

        public async Task<Video> UpdateVideoAsync(Video video)
        {
            _context.Videos.Update(video);
            await _context.SaveChangesAsync();
            return video;
        }

        public async Task<bool> DeleteVideoAsync(int videoId)
        {
            var video = await _context.Videos.FindAsync(videoId);
            if (video == null)
                return false;

            video.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}