using FoundersDesk.Models;

namespace FoundersDesk.Interfaces
{
    public interface IResourceRepository
    {
        Document GetDocumentById(int documentId);

        ResourceAcknowledgement GetAcknowledgement(string username, int documentId);

        void AddAcknowledgement(ResourceAcknowledgement acknowledgement);

        void UpdateAcknowledgement(ResourceAcknowledgement acknowledgement);

        void Save();
    }
}
