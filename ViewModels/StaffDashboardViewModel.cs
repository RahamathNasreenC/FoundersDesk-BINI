using System;
using System.Collections.Generic;
using FoundersDesk.DTOs;

namespace FoundersDesk.ViewModels
{
    public class StaffDashboardViewModel
    {
        public UserDto User { get; set; }
        public List<CourseViewModel> Courses { get; set; }

        // ===== Company Policies =====
        public CompanyPolicyViewModel CompanyPolicies { get; set; }

        // ===== Welcome Kit =====
        public List<TrainingResourceViewModel> TrainingResources { get; set; }

        // ===== Signature =====
        public bool IsSignatureUploaded { get; set; }
        public DateTime? SignatureUploadedAt { get; set; }
    }
}
