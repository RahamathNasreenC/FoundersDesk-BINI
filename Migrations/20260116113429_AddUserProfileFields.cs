using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoundersDesk.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 16);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsProfileCompleted",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoPath",
                table: "Users",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsProfileCompleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoPath",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "VideoId", "Category", "Description", "DisplayOrder", "Icon", "IsActive", "RoleType", "Title", "VideoUrl" },
                values: new object[,]
                {
                    { 1, "orientation", "Platform overview", 1, "🎉", true, "general", "Welcome to Founder's Desk", "https://youtu.be/1" },
                    { 2, "orientation", "Rules & conduct", 2, "📘", true, "general", "Intern Guidelines", "https://youtu.be/2" },
                    { 3, "projects", "What you'll work on", 3, "📂", true, "general", "Current Projects", "https://youtu.be/3" },
                    { 4, "culture", "Our values", 4, "🤝", true, "general", "Company Culture", "https://youtu.be/4" },
                    { 5, "technical", "Tools & frameworks", 5, "💻", true, "technical", "Tech Stack Intro", "https://youtu.be/5" },
                    { 6, "technical", "Best practices", 6, "🧠", true, "technical", "Coding Standards", "https://youtu.be/6" },
                    { 7, "non-technical", "Client & team communication", 7, "🗣️", true, "non-technical", "Communication Skills", "https://youtu.be/7" },
                    { 8, "non-technical", "Work smart", 8, "⏱️", true, "non-technical", "Time Management", "https://youtu.be/8" },
                    { 11, "vendor", "How vendors use the platform", 1, "🏢", true, "vendor", "Vendor Overview", "https://youtu.be/9" },
                    { 12, "vendor", "Post openings + requirements", 2, "📄", true, "vendor", "Adding Requirements", "https://youtu.be/10" },
                    { 13, "vendor", "Allocate work to interns", 3, "🗂️", true, "vendor", "Assign Projects", "https://youtu.be/11" },
                    { 14, "vendor", "Monitor activities & progress", 4, "📊", true, "vendor", "Track Intern Work", "https://youtu.be/12" },
                    { 15, "vendor", "Chat & meetings", 5, "💬", true, "vendor", "Communicate with Interns", "https://youtu.be/13" },
                    { 16, "vendor", "Billing + agreements", 6, "💼", true, "vendor", "Payment & Contracts", "https://youtu.be/14" }
                });
        }
    }
}
