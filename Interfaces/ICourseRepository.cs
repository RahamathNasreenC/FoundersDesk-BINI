using FoundersDesk.Models;
using System.Threading.Tasks;

namespace FoundersDesk.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course> GetCourseWithModulesAsync(int courseId);
    }
}
