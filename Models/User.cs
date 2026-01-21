using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersDesk.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        // ✅ NEW
        [StringLength(150)]
        public string FullName { get; set; }

        // ✅ NEW
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        // ✅ NEW
        [StringLength(100)]
        public string? JobRole { get; set; }   // nullable





        // ✅ NEW
        public bool IsProfileCompleted { get; set; } = false;


        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
