using System;

namespace FoundersDesk.Models
{
    public class DigitalSignature
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FilePath { get; set; }

        public DateTime UploadedAt { get; set; }
    }
}
