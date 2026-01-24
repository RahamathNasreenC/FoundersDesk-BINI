namespace FoundersDesk.DTOs
{
    public class ModuleDto
    {
        public string Title { get; set; }
        public List<VideoDto> Videos { get; set; } = new();
    }

}
