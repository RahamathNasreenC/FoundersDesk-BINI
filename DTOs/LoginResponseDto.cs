using FoundersDesk.DTOs;

namespace FoundersDesk.DTOs
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserDto User { get; set; }
        public string RedirectUrl { get; set; }
    }
}
