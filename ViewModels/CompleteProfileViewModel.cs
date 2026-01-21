using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FoundersDesk.ViewModels
{
    public class CompleteProfileViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        public string PhoneNumber { get; set; }

        // Changed from IFormFile to string
        public string? ProfilePhoto { get; set; }
        

    }
}
