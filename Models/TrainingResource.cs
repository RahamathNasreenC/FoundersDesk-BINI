using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoundersDesk.Models
{
    public class TrainingResource
    {
        [Key]
        public int TrainingResourceId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        // PPT file path
        [Required]
        public string FileUrl { get; set; }

        // Staff / Intern
        [Required]
        public string RoleType { get; set; }

        // Frontend / Backend / HR etc
        // NULL = visible to all job roles
        public string? JobRole { get; set; }

        // Generic PPT or role-specific
        public bool IsGeneric { get; set; }

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        

    }
}
