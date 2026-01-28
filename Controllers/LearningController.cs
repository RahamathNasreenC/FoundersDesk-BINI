using Microsoft.AspNetCore.Mvc;
using FoundersDesk.Interfaces;

namespace FoundersDesk.Controllers
{
    public class LearningController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public LearningController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Course(int id)
        {
            var course = await _courseRepository.GetCourseWithModulesAsync(id);

            if (course == null)
                return NotFound();

            return View(course);
        }
    }
}
