using System.Collections.Generic;
using FoundersDesk.DTOs;

namespace FoundersDesk.ViewModels
{
    public class InternDashboardViewModel
    {
        public UserDto User { get; set; }
        public List<VideoDto> OrientationVideos { get; set; }
        public List<VideoDto> ProjectVideos { get; set; }
        public List<VideoDto> CultureVideos { get; set; }
        public List<VideoDto> TechnicalVideos { get; set; }
        public List<VideoDto> NonTechnicalVideos { get; set; }
        public List<string> AcknowledgedResources { get; set; } = new List<string>();
        public bool IsSignatureUploaded { get; set; }
        public DateTime? SignatureUploadedAt { get; set; }


    }
}
