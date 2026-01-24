using System.Collections.Generic;
using FoundersDesk.DTOs;

namespace FoundersDesk.ViewModels
{
    public class ModuleViewModel
    {
        public int ModuleId { get; set; }
        public string Title { get; set; }

        public List<VideoDto> Videos { get; set; }
    }
}
