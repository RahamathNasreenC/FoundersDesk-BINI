using FoundersDesk.Data;

using FoundersDesk.Models;

namespace FoundersDesk.Data
{
    public static class DbSeeder
    {
        public static void SeedCourses(ApplicationDbContext context)
        {
            if (context.Courses.Any()) return;

            // =========================
            // STAFF - FRONTEND COURSE
            // =========================
            var staffFrontendCourse = new Course
            {
                Title = "Staff Frontend Onboarding",
                Description = "Complete frontend onboarding for staff",
                RoleType = "Staff",
                JobRole = "Frontend",
                DisplayOrder = 1,
                IsActive = true,
                Modules = new List<Module>()
            };

            var feModule1 = new Module
            {
                Title = "Company Introduction",
                DisplayOrder = 1,
                Videos = new List<Video>()
            };

            feModule1.Videos.Add(new Video
            {
                Title = "Welcome to Founders Desk",
                Description = "Company introduction",
                Category = "WelcomeKit",
                RoleType = "Staff",
                JobRole = "Frontend",
                Icon = "🎉",
                VideoUrl = "/videos/common/welcome.mp4",
                DisplayOrder = 1,
                IsActive = true
            });

            feModule1.Videos.Add(new Video
            {
                Title = "Company Rules",
                Description = "Policies and conduct",
                Category = "WelcomeKit",
                RoleType = "Staff",
                JobRole = "Frontend",
                Icon = "📜",
                VideoUrl = "/videos/common/rules.mp4",
                DisplayOrder = 2,
                IsActive = true
            });

            var feModule2 = new Module
            {
                Title = "Technical Setup",
                DisplayOrder = 2,
                Videos = new List<Video>()
            };

            feModule2.Videos.Add(new Video
            {
                Title = "Project Structure",
                Description = "Frontend folder structure",
                Category = "WelcomeKit",
                RoleType = "Staff",
                JobRole = "Frontend",
                Icon = "🧱",
                VideoUrl = "/videos/frontend/structure.mp4",
                DisplayOrder = 1,
                IsActive = true
            });

            feModule2.Videos.Add(new Video
            {
                Title = "UI Guidelines",
                Description = "Design system standards",
                Category = "WelcomeKit",
                RoleType = "Staff",
                JobRole = "Frontend",
                Icon = "🎨",
                VideoUrl = "/videos/frontend/ui.mp4",
                DisplayOrder = 2,
                IsActive = true
            });

            staffFrontendCourse.Modules.Add(feModule1);
            staffFrontendCourse.Modules.Add(feModule2);

            // =========================
            // STAFF - BACKEND COURSE
            // =========================
            var staffBackendCourse = new Course
            {
                Title = "Staff Backend Onboarding",
                Description = "Complete backend onboarding for staff",
                RoleType = "Staff",
                JobRole = "Backend",
                DisplayOrder = 2,
                IsActive = true,
                Modules = new List<Module>()
            };

            var beModule1 = new Module
            {
                Title = "Backend Architecture",
                DisplayOrder = 1,
                Videos = new List<Video>()
            };

            beModule1.Videos.Add(new Video
            {
                Title = "System Architecture",
                Description = "Backend system design",
                Category = "WelcomeKit",
                RoleType = "Staff",
                JobRole = "Backend",
                Icon = "🧠",
                VideoUrl = "/videos/backend/architecture.mp4",
                DisplayOrder = 1,
                IsActive = true
            });

            staffBackendCourse.Modules.Add(beModule1);

            // =========================
            // INTERN - FRONTEND COURSE
            // =========================
            var internFrontendCourse = new Course
            {
                Title = "Intern Frontend Onboarding",
                Description = "Frontend basics for interns",
                RoleType = "Intern",
                JobRole = "Frontend",
                DisplayOrder = 3,
                IsActive = true,
                Modules = new List<Module>()
            };

            var ifeModule1 = new Module
            {
                Title = "Getting Started",
                DisplayOrder = 1,
                Videos = new List<Video>()
            };

            ifeModule1.Videos.Add(new Video
            {
                Title = "Intern Welcome",
                Description = "Introduction for interns",
                Category = "WelcomeKit",
                RoleType = "Intern",
                JobRole = "Frontend",
                Icon = "👶",
                VideoUrl = "/videos/intern/frontend_intro.mp4",
                DisplayOrder = 1,
                IsActive = true
            });

            internFrontendCourse.Modules.Add(ifeModule1);

            // =========================
            // INTERN - BACKEND COURSE
            // =========================
            var internBackendCourse = new Course
            {
                Title = "Intern Backend Onboarding",
                Description = "Backend basics for interns",
                RoleType = "Intern",
                JobRole = "Backend",
                DisplayOrder = 4,
                IsActive = true,
                Modules = new List<Module>()
            };

            var ibeModule1 = new Module
            {
                Title = "API Basics",
                DisplayOrder = 1,
                Videos = new List<Video>()
            };

            ibeModule1.Videos.Add(new Video
            {
                Title = "Backend Intern Intro",
                Description = "API and tools overview",
                Category = "WelcomeKit",
                RoleType = "Intern",
                JobRole = "Backend",
                Icon = "⚙️",
                VideoUrl = "/videos/intern/backend_intro.mp4",
                DisplayOrder = 1,
                IsActive = true
            });

            internBackendCourse.Modules.Add(ibeModule1);

            // =========================
            // SAVE ALL
            // =========================
            context.Courses.AddRange(
                staffFrontendCourse,
                staffBackendCourse,
                internFrontendCourse,
                internBackendCourse
            );

            context.SaveChanges();
        }
        public static void SeedTrainingResources(ApplicationDbContext context)
        {
            if (context.TrainingResources.Any()) return;

            var resources = new List<TrainingResource>
    {
        // =========================
        // GENERIC – STAFF (ALL ROLES)
        // =========================
        new TrainingResource
        {
            Title = "Company Overview",
            Description = "Introduction to Founders Desk",
            FileUrl = "/uploads/training-resources/company-overview.pptx",
            RoleType = "Staff",
            JobRole = null,          // visible to all roles
            IsGeneric = true,
            DisplayOrder = 1,
            IsActive = true
        },

        // =========================
        // FRONTEND – STAFF ONLY
        // =========================
        new TrainingResource
        {
            Title = "Frontend Basics",
            Description = "Frontend standards and workflow",
            FileUrl = "/uploads/training-resources/frontend-basics.pptx",
            RoleType = "Staff",
            JobRole = "Frontend",
            IsGeneric = false,
            DisplayOrder = 2,
            IsActive = true
        }
    };

            context.TrainingResources.AddRange(resources);
            context.SaveChanges();
        }



    }
}
