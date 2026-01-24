namespace FoundersDesk.DTOs
{
    public class CourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ModuleDto> Modules { get; set; } = new();
    }

}
