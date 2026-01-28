using FoundersDesk.Data;
using FoundersDesk.Interfaces;
using FoundersDesk.Models;
using System.Linq;

namespace FoundersDesk.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationDbContext _context;

        public ResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Document GetDocumentById(int documentId)
        {
            return _context.Documents.Find(documentId);
        }

        public ResourceAcknowledgement GetAcknowledgement(string username, int documentId)
        {
            return _context.ResourceAcknowledgements
                .FirstOrDefault(r => r.Username == username && r.DocumentId == documentId);
        }

        public void AddAcknowledgement(ResourceAcknowledgement acknowledgement)
        {
            _context.ResourceAcknowledgements.Add(acknowledgement);
        }

        public void UpdateAcknowledgement(ResourceAcknowledgement acknowledgement)
        {
            _context.ResourceAcknowledgements.Update(acknowledgement);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
