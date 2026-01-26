using System;
using System.Collections.Generic;

namespace FoundersDesk.ViewModels
{
    public class CompanyPolicyViewModel
    {
        public List<PolicyDocumentItem> Documents { get; set; }
    }

    public class PolicyDocumentItem
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public bool RequiresSignature { get; set; }
        public bool IsAcknowledged { get; set; }
        public DateTime? AcknowledgedAt { get; set; }
    }
}
