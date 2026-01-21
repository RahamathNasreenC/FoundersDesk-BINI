namespace FoundersDesk.DTOs
{
    public class VideoDto
    {
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string RoleType { get; set; }

        // ✅ Make nullable to match Video model
        public string? VideoUrl { get; set; }

        public string Icon { get; set; }
    }
}
