using Microsoft.AspNetCore.Mvc;
using FoundersDesk.Interfaces;
using FoundersDesk.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace FoundersDesk.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceController(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        [HttpPost]
        public IActionResult Acknowledge(int documentId)
        {
            try
            {
                var username = HttpContext.Session.GetString("Username");

                if (string.IsNullOrEmpty(username))
                    return Json(new { success = false, message = "Session expired." });

                var document = _resourceRepository.GetDocumentById(documentId);

                if (document == null)
                    return Json(new { success = false, message = "Document not found." });

                var acknowledgement =
                    _resourceRepository.GetAcknowledgement(username, documentId);

                if (acknowledgement == null)
                {
                    acknowledgement = new ResourceAcknowledgement
                    {
                        Username = username,
                        DocumentId = documentId,
                        AcknowledgedAt = DateTime.UtcNow
                    };

                    _resourceRepository.AddAcknowledgement(acknowledgement);
                }
                else if (acknowledgement.AcknowledgedAt < document.UpdatedAt)
                {
                    acknowledgement.AcknowledgedAt = DateTime.UtcNow;
                    _resourceRepository.UpdateAcknowledgement(acknowledgement);
                }
                else
                {
                    return Json(new { success = false, message = "Already acknowledged." });
                }

                _resourceRepository.Save();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Content("SERVER ERROR: " + ex.Message);
            }
        }
    }
}
