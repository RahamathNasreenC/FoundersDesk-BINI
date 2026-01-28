using FoundersDesk.Data;
using FoundersDesk.Interfaces;
using FoundersDesk.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class VideoRepository : IVideoRepository
{
    private readonly ApplicationDbContext _context;

    public VideoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Video>> GetVideosByModuleAsync(int moduleId)
    {
        return await _context.Videos
            .Where(v => v.ModuleId == moduleId && v.IsActive)
            .OrderBy(v => v.DisplayOrder)
            .ToListAsync();
    }
}
