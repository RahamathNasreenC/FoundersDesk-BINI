using FoundersDesk.Repositories.Interfaces;
using FoundersDesk.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FoundersDesk.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ITrainingResourceRepository _trainingRepo;

        public TrainingController(ITrainingResourceRepository trainingRepo)
        {
            _trainingRepo = trainingRepo;
        }

        public IActionResult Index(string pageKey)
        {
            if (string.IsNullOrWhiteSpace(pageKey))
                return NotFound();

            var resources = _trainingRepo.GetActiveResources();
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
