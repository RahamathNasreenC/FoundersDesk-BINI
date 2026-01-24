using System.Collections.Generic;

namespace FoundersDesk.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<ModuleViewModel> Modules { get; set; }
    }
}
