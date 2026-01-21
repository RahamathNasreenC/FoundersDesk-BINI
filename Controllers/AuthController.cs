using FoundersDesk.DTOs;
using FoundersDesk.Interfaces;
using FoundersDesk.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoundersDesk.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthRepository authRepository, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
        }

        // GET: /Auth/Login?role=intern or /Auth/Login?role=staff
        [HttpGet]
        public IActionResult Login(string role = "intern")
        {
            var viewModel = new LoginViewModel
            {
                Role = role.ToLower()
            };

            if (role.ToLower() == "staff")
            {
                viewModel.Heading = "Staff Login";
                viewModel.SubText = "Login with your staff credentials to access staff tools.";
            }
            else
            {
                viewModel.Heading = "Intern Login";
                viewModel.SubText = "Use your intern  credentials to access onboarding and tasks.";
            }

            return View(viewModel);
        }

        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest, string role = "intern")

        {
            if (!ModelState.IsValid)
            {
                var viewModel = new LoginViewModel
                {
                    Role = role,
                    Heading = role == "staff" ? "Staff Login" : "Intern Login",
                    SubText = role == "staff"
        ? "Login with your staff credentials to access staff tools."
        : "Use your intern credentials to access onboarding and tasks."
                };
              


                return View(viewModel);
            }

            try
            {
                // Authenticate user
                var user = await _authRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    var errorViewModel = new LoginViewModel
                    {
                        Role = role,
                        Heading = role == "staff" ? "Staff Login" : "Intern Login",
                        SubText = role == "staff"
         ? "Login with your staff credentials to access staff tools."
         : "Use your intern credentials to access onboarding and tasks."
                    };
                    return View(errorViewModel);

                    return View(errorViewModel);
                }

                // Create session
                var session = await _authRepository.CreateSessionAsync(user.UserId);

                // Update last login
                await _userRepository.UpdateLastLoginAsync(user.UserId);

                // Store session in cookie/session
                HttpContext.Session.SetString("SessionToken", session.SessionToken);
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());
                HttpContext.Session.SetString("Username", user.Username);


                // 🔒 Force profile completion
                if (!user.IsProfileCompleted)
                {
                    return RedirectToAction("Complete", "Profile");
                }

                // Redirect based on role
                if (user.Role == Models.UserRole.Staff)
                {
                    return RedirectToAction("Dashboard", "Staff");
                }
                else
                {
                    return RedirectToAction("Dashboard", "Intern");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred during login. Please try again.");
                var errorViewModel = new LoginViewModel
                {
                    Role = role,
                    Heading = role == "staff" ? "Staff Login" : "Intern Login",
                    SubText = role == "staff"
    ? "Login with your staff credentials to access staff tools."
    : "Use your intern credentials to access onboarding and tasks."

                };
                return View(errorViewModel);
            }
        }

        // GET: /Auth/Logout
        public async Task<IActionResult> Logout()
        {
            var sessionToken = HttpContext.Session.GetString("SessionToken");
            if (!string.IsNullOrEmpty(sessionToken))
            {
                await _authRepository.InvalidateSessionAsync(sessionToken);
            }

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // POST: /Auth/Register (Optional - if you want registration)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Check if user already exists
                if (await _authRepository.UserExistsAsync(registerRequest.Username, registerRequest.Email))
                {
                    ModelState.AddModelError("", "Username or email already exists");
                    return BadRequest(ModelState);
                }

                // Register user
                var user = await _authRepository.RegisterAsync(registerRequest);

                // Create session
                var session = await _authRepository.CreateSessionAsync(user.UserId);

                // Store session
                HttpContext.Session.SetString("SessionToken", session.SessionToken);
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());
                HttpContext.Session.SetString("Username", user.Username);


                // Redirect based on role
                if (user.Role == Models.UserRole.Staff)
                {
                    return RedirectToAction("Dashboard", "Staff");
                }
                else
                {
                    return RedirectToAction("Dashboard", "Intern");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred during registration. Please try again.");
                return BadRequest(ModelState);
            }
        }
    }
}
