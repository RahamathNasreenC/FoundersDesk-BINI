using System;
using System.Collections.Generic;
using FoundersDesk.DTOs;

namespace FoundersDesk.ViewModels
{
    public class StaffDashboardViewModel
    {
        public UserDto User { get; set; }
        public List<VideoDto> PlatformVideos { get; set; }

        // ===== Resources & Signature =====
        public List<string> AcknowledgedResources { get; set; } = new List<string>();
        public bool IsSignatureUploaded { get; set; }
        public DateTime? SignatureUploadedAt { get; set; }
    }
}
