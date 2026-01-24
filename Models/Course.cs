using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FoundersDesk.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        public string RoleType { get; set; }   // Staff / Intern

        public string? JobRole { get; set; }   // Frontend / Backend

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }

        public List<Module> Modules { get; set; }
    }
}
