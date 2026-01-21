using System;

namespace FoundersDesk.Models
{
    public class ResourceAcknowledgement
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string ResourceType { get; set; }  // terms / privacy / conduct

        public DateTime AcknowledgedAt { get; set; }
    }
}
