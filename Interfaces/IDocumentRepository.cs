using FoundersDesk.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoundersDesk.Interfaces
{
    public interface IDocumentRepository
    {
        Task<List<Document>> GetDocumentsByRoleAsync(string roleType);
        Task<Document> GetDocumentByTypeAsync(string documentType);
    }
}
