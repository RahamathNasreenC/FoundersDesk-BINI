using FoundersDesk.Data;
using FoundersDesk.DTOs;
using FoundersDesk.Interfaces;
using FoundersDesk.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FoundersDesk.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // HASH PASSWORD
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        // AUTHENTICATE
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Username == username &&
                    u.PasswordHash == hashedPassword &&
                    u.IsActive);
        }

        // REGISTER
        public async Task<User> RegisterAsync(RegisterRequestDto registerDto)
        {
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password),
                Role = registerDto.Role.ToLower() == "staff" ? UserRole.Staff : UserRole.Intern,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users.AnyAsync(u =>
                u.Username == username || u.Email == email);
        }

        public async Task<Session> CreateSessionAsync(int userId)
        {
            var session = new Session
            {
                UserId = userId,
                SessionToken = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                IsActive = true
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<bool> ValidateSessionAsync(string sessionToken)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s =>
                    s.SessionToken == sessionToken && s.IsActive);

            return session != null && session.ExpiresAt > DateTime.UtcNow;
        }

        public async Task InvalidateSessionAsync(string sessionToken)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.SessionToken == sessionToken);

            if (session != null)
            {
                session.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
