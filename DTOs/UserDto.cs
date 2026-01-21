namespace FoundersDesk.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        // ✅ NEW
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string JobRole { get; set; }


        public bool IsProfileCompleted { get; set; }
        public string Role { get; set; }
    }
}
