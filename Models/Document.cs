using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersDesk.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public string DocumentType { get; set; }
        // values: "terms", "privacy", "conduct"

        public DateTime UpdatedAt { get; set; }
        // when the document content was last changed
    }
}
