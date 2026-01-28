using FoundersDesk.Models;

namespace FoundersDesk.Repositories.Interfaces
{
    public interface ITrainingResourceRepository
    {
        List<TrainingResource> GetActiveResources();
        TrainingResource? GetByPageKey(string pageKey);
    }
}
