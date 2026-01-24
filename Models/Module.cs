using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoundersDesk.Models
{
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }

        [Required]
        public string Title { get; set; }

        public int DisplayOrder { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public List<Video> Videos { get; set; }
    }
}
