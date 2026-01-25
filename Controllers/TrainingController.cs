using FoundersDesk.Data;
using FoundersDesk.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FoundersDesk.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string pageKey)
        {
            if (string.IsNullOrWhiteSpace(pageKey))
                return NotFound();

            var resources = _context.TrainingResources
                .Where(r => r.IsActive)
                .OrderBy(r => r.DisplayOrder)
                .ToList();

            var currentIndex = resources.FindIndex(r => r.PageKey == pageKey);

            if (currentIndex == -1)
                return NotFound();

            var model = new TrainingPageViewModel
            {
                Title = resources[currentIndex].Title,
                PageKey = pageKey,
                PreviousPageKey = currentIndex > 0
                    ? resources[currentIndex - 1].PageKey
                    : null,
                NextPageKey = currentIndex < resources.Count - 1
                    ? resources[currentIndex + 1].PageKey
                    : null
            };

            return View($"~/Views/Training/{pageKey}.cshtml", model);
        }
    }
}
