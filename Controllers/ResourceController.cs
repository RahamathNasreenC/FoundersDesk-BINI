using Microsoft.AspNetCore.Mvc;
using FoundersDesk.Data;
using FoundersDesk.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;



namespace FoundersDesk.Controllers
{
    public class ResourceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResourceController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        [HttpPost]
        public IActionResult Acknowledge(int documentId)
        {
            try
            {
                var username = HttpContext.Session.GetString("Username");

                if (string.IsNullOrEmpty(username))
                    return Json(new { success = false, message = "Session expired." });

                var document = _context.Documents.Find(documentId);

                if (document == null)
                    return Json(new { success = false, message = "Document not found." });

                var acknowledgement = _context.ResourceAcknowledgements
                    .FirstOrDefault(r => r.Username == username && r.DocumentId == documentId);

                // First time acknowledgement
                if (acknowledgement == null)
                {
                    acknowledgement = new ResourceAcknowledgement
                    {
                        Username = username,
                        DocumentId = documentId,
                        AcknowledgedAt = DateTime.UtcNow
                    };

                    _context.ResourceAcknowledgements.Add(acknowledgement);
                }
                // Re-acknowledgement after document update
                else if (acknowledgement.AcknowledgedAt < document.UpdatedAt)
                {
                    acknowledgement.AcknowledgedAt = DateTime.UtcNow;
                }
                else
                {
                    return Json(new { success = false, message = "Already acknowledged." });
                }

                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Content("SERVER ERROR: " + ex.Message);
            }
        }

    }
}
