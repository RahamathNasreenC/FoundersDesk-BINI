using FoundersDesk.Models;

namespace FoundersDesk.Data
{
    public static class DbSeeder
    {
        public static void SeedVideos(ApplicationDbContext context)
        {
            if (context.Videos.Any()) return;

            context.Videos.AddRange(
                new Video { VideoId = 1, Title = "Welcome to Founder's Desk", Description = "Platform overview", RoleType = "general", Category = "orientation", Icon = "🎉", VideoUrl = "https://youtu.be/1", IsActive = true, DisplayOrder = 1 },
                new Video { VideoId = 2, Title = "Intern Guidelines", Description = "Rules & conduct", RoleType = "general", Category = "orientation", Icon = "📘", VideoUrl = "https://youtu.be/2", IsActive = true, DisplayOrder = 2 },
                new Video { VideoId = 3, Title = "Current Projects", Description = "What you'll work on", RoleType = "general", Category = "projects", Icon = "📂", VideoUrl = "https://youtu.be/3", IsActive = true, DisplayOrder = 3 },
                new Video { VideoId = 4, Title = "Company Culture", Description = "Our values", RoleType = "general", Category = "culture", Icon = "🤝", VideoUrl = "https://youtu.be/4", IsActive = true, DisplayOrder = 4 },

                new Video { VideoId = 5, Title = "Tech Stack Intro", Description = "Tools & frameworks", RoleType = "technical", Category = "technical", Icon = "💻", VideoUrl = "https://youtu.be/5", IsActive = true, DisplayOrder = 5 },
                new Video { VideoId = 6, Title = "Coding Standards", Description = "Best practices", RoleType = "technical", Category = "technical", Icon = "🧠", VideoUrl = "https://youtu.be/6", IsActive = true, DisplayOrder = 6 },

                new Video { VideoId = 7, Title = "Communication Skills", Description = "Client & team communication", RoleType = "non-technical", Category = "non-technical", Icon = "🗣️", VideoUrl = "https://youtu.be/7", IsActive = true, DisplayOrder = 7 },
                new Video { VideoId = 8, Title = "Time Management", Description = "Work smart", RoleType = "non-technical", Category = "non-technical", Icon = "⏱️", VideoUrl = "https://youtu.be/8", IsActive = true, DisplayOrder = 8 },

                new Video { VideoId = 11, Title = "Vendor Overview", Description = "How vendors use the platform", RoleType = "vendor", Category = "vendor", Icon = "🏢", VideoUrl = "https://youtu.be/9", IsActive = true, DisplayOrder = 1 },
                new Video { VideoId = 12, Title = "Adding Requirements", Description = "Post openings + requirements", RoleType = "vendor", Category = "vendor", Icon = "📄", VideoUrl = "https://youtu.be/10", IsActive = true, DisplayOrder = 2 },
                new Video { VideoId = 13, Title = "Assign Projects", Description = "Allocate work to interns", RoleType = "vendor", Category = "vendor", Icon = "🗂️", VideoUrl = "https://youtu.be/11", IsActive = true, DisplayOrder = 3 },
                new Video { VideoId = 14, Title = "Track Intern Work", Description = "Monitor activities & progress", RoleType = "vendor", Category = "vendor", Icon = "📊", VideoUrl = "https://youtu.be/12", IsActive = true, DisplayOrder = 4 },
                new Video { VideoId = 15, Title = "Communicate with Interns", Description = "Chat & meetings", RoleType = "vendor", Category = "vendor", Icon = "💬", VideoUrl = "https://youtu.be/13", IsActive = true, DisplayOrder = 5 },
                new Video { VideoId = 16, Title = "Payment & Contracts", Description = "Billing + agreements", RoleType = "vendor", Category = "vendor", Icon = "💼", VideoUrl = "https://youtu.be/14", IsActive = true, DisplayOrder = 6 }
            );

            context.SaveChanges();
        }
    }
}
