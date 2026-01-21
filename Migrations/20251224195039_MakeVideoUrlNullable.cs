using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoundersDesk.Migrations
{
    /// <inheritdoc />
    public partial class MakeVideoUrlNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 15);

            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "Videos",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1,
                columns: new[] { "Description", "Icon", "VideoUrl" },
                values: new object[] { "Platform overview", "🎉", null });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2,
                columns: new[] { "Description", "Icon", "Title", "VideoUrl" },
                values: new object[] { "Rules & conduct", "📘", "Intern Guidelines", null });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3,
                columns: new[] { "Category", "Description", "DisplayOrder", "Icon", "Title", "VideoUrl" },
                values: new object[] { "projects", "What you'll work on", 1, "📂", "Current Projects", null });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "VideoId", "Category", "Description", "DisplayOrder", "Icon", "IsActive", "RoleType", "Title", "VideoUrl" },
                values: new object[,]
                {
                    { 4, "culture", "Our values", 1, "🤝", true, "general", "Company Culture", null },
                    { 5, "technical", "Tools & frameworks", 1, "💻", true, "technical", "Tech Stack Intro", null },
                    { 6, "technical", "Best practices", 2, "🧠", true, "technical", "Coding Standards", null },
                    { 7, "non-technical", "Client & team talks", 1, "🗣️", true, "non-technical", "Communication Skills", null },
                    { 8, "non-technical", "Work smart", 2, "⏱️", true, "non-technical", "Time Management", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoUrl",
                keyValue: null,
                column: "VideoUrl",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "Videos",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1,
                columns: new[] { "Description", "Icon", "VideoUrl" },
                values: new object[] { "Company intro", "🎯", "https://example.com/video1.mp4" });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2,
                columns: new[] { "Description", "Icon", "Title", "VideoUrl" },
                values: new object[] { "Rules & guidelines", "📚", "Company Policies", "https://example.com/video2.mp4" });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3,
                columns: new[] { "Category", "Description", "DisplayOrder", "Icon", "Title", "VideoUrl" },
                values: new object[] { "orientation", "Working together", 3, "🤝", "Team Collaboration", "https://example.com/video3.mp4" });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "VideoId", "Category", "Description", "DisplayOrder", "Icon", "IsActive", "RoleType", "Title", "VideoUrl" },
                values: new object[,]
                {
                    { 14, "platform", "Vendor onboarding", 1, "🚀", true, "vendor", "Platform Introduction", "https://example.com/video14.mp4" },
                    { 15, "platform", "Vendor app usage", 2, "📱", true, "vendor", "Using the App", "https://example.com/video15.mp4" }
                });
        }
    }
}
