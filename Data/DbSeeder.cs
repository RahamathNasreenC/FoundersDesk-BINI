using FoundersDesk.Models;

namespace FoundersDesk.Data
{
    public static class DbSeeder
    {
        public static void SeedVideos(ApplicationDbContext context)
        {
            if (context.Videos.Any()) return;

            context.Videos.AddRange(

                // ======================
                // COMMON FOR ALL STAFF
                // ======================
                new Video
                {
                    Title = "Welcome to Founders Desk",
                    Description = "Company introduction",
                    RoleType = "Staff",
                    JobRole = null,   // null = visible to all staff
                    Category = "WelcomeKit",
                    Icon = "🎉",
                    VideoUrl = "/videos/common/welcome.mp4",
                    DisplayOrder = 1,
                    IsActive = true

                },

                // ======================
                // FRONTEND STAFF
                // ======================
                new Video
                {
                    Title = "Frontend Code Structure",
                    Description = "Project folder architecture",
                    RoleType = "Staff",
                    JobRole = "Frontend",
                    Category = "WelcomeKit",
                    Icon = "🎨",
                    VideoUrl = "/videos/frontend/structure.mp4",
                    DisplayOrder = 1,
                    IsActive = true

                },
                new Video
                {
                    Title = "UI Guidelines",
                    Description = "Design system & standards",
                    RoleType = "Staff",
                    JobRole = "Frontend",
                    Category = "WelcomeKit",
                    Icon = "🧩",
                    VideoUrl = "/videos/frontend/ui.mp4",
                    DisplayOrder = 2,
                    IsActive = true

                },

                // ======================
                // BACKEND STAFF
                // ======================
                new Video
                {
                    Title = "Backend Architecture",
                    Description = "System design walkthrough",
                    RoleType = "Staff",
                    JobRole = "Backend",
                    Category = "WelcomeKit",
                    Icon = "🧠",
                    VideoUrl = "/videos/backend/architecture.mp4",
                    DisplayOrder = 1,
                    IsActive = true

                },

                // ======================
                // INTERN - FRONTEND
                // ======================
                new Video
                {
                    Title = "Intern Frontend Onboarding",
                    Description = "Basics for interns",
                    RoleType = "Intern",
                    JobRole = "Frontend",
                    Category = "WelcomeKit",
                    Icon = "👶",
                    VideoUrl = "/videos/intern/frontend_intro.mp4",
                    DisplayOrder = 1,
                    IsActive = true

                },

                // ======================
                // INTERN - BACKEND
                // ======================
                new Video
                {
                    Title = "Intern Backend Onboarding",
                    Description = "API basics & tools",
                    RoleType = "Intern",
                    JobRole = "Backend",
                    Category = "WelcomeKit",
                    Icon = "⚙️",
                    VideoUrl = "/videos/intern/backend_intro.mp4",
                    DisplayOrder = 1,
                    IsActive = true

                }
            );

            context.SaveChanges();
        }
    }
}
