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

            var resource = _context.TrainingResources
                .Where(r => r.IsActive)
                .OrderBy(r => r.DisplayOrder)
                .ToList();

            var currentIndex = resource.FindIndex(r => r.PageKey == pageKey);

            if (currentIndex == -1)
                return NotFound();

            var model = new TrainingPageViewModel
            {
                Title = resource[currentIndex].Title,
                PageKey = pageKey,
                PreviousPageKey = currentIndex > 0
                    ? resource[currentIndex - 1].PageKey
                    : null,
                NextPageKey = currentIndex < resource.Count - 1
                    ? resource[currentIndex + 1].PageKey
                    : null
            };

            return View(pageKey, model);
        }
    }
}
