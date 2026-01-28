using FoundersDesk.Data;
using FoundersDesk.Interfaces;
using FoundersDesk.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Course> GetCourseWithModulesAsync(int courseId)
    {
        return await _context.Courses
            .Include(c => c.Modules)
                .ThenInclude(m => m.Videos)
            .FirstOrDefaultAsync(c => c.CourseId == courseId && c.IsActive);
    }
}
