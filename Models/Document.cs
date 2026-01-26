using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersDesk.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public string Title { get; set; }        // "Company Policy", "NDA", etc

        [Required]
        public string DocumentType { get; set; } // unique key: "company-policy", "nda"

        [Required]
        public string FilePath { get; set; }     // "/docs/staff/company-policy.pdf"

        [Required]
        public string RoleType { get; set; }     // "Staff" / "Intern"

        public bool RequiresSignature { get; set; }  // Staff=true, Intern=false

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}
