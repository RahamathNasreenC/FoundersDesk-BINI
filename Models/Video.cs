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
        public string Category { get; set; } // orientation, projects, culture, technical, etc.

        [Required]
        [StringLength(50)]
        public string RoleType { get; set; } // general, technical, non-technical, vendor

        // 🔹 ONLY CHANGE: make VideoUrl nullable (no [Required])
        [StringLength(255)]
        public string? VideoUrl { get; set; }

        [StringLength(50)]
        public string Icon { get; set; } // Emoji or icon class

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }
    }
}
