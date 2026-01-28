using FoundersDesk.Data;
using FoundersDesk.Interfaces;
using FoundersDesk.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundersDesk.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Document>> GetDocumentsByRoleAsync(string roleType)
        {
            return await _context.Documents
                .Where(d => d.IsActive && d.RoleType == roleType)
                .OrderByDescending(d => d.UpdatedAt)
                .ToListAsync();
        }

        public async Task<Document> GetDocumentByTypeAsync(string documentType)
        {
            return await _context.Documents
                .FirstOrDefaultAsync(d => d.DocumentType == documentType && d.IsActive);
        }
    }
}
