using FoundersDesk.Data;
using FoundersDesk.Models;
using FoundersDesk.Repositories.Interfaces;

namespace FoundersDesk.Repositories
{
    public class TrainingResourceRepository : ITrainingResourceRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TrainingResource> GetActiveResources()
        {
            return _context.TrainingResources
                .Where(r => r.IsActive)
                .OrderBy(r => r.DisplayOrder)
                .ToList();
        }

        public TrainingResource? GetByPageKey(string pageKey)
        {
            return _context.TrainingResources
                .FirstOrDefault(r => r.PageKey == pageKey && r.IsActive);
        }
    }
}
