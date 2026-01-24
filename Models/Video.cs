using System.ComponentModel.DataAnnotations;

namespace FoundersDesk.Models
{
    public class Video
    {
        [Key]
        public int VideoId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleType { get; set; } // Staff / Intern

        [StringLength(100)]
        public string? JobRole { get; set; } // Frontend, Backend, HR, etc

        [StringLength(255)]
        public string? VideoUrl { get; set; }  // Local mp4 path or stream URL

        [StringLength(50)]
        public string Icon { get; set; }

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }

    }
}
