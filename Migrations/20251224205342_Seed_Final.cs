using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoundersDesk.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "IsActive", "LastLogin", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 101, new DateTime(2025, 12, 24, 20, 53, 40, 600, DateTimeKind.Utc).AddTicks(6112), "intern1@example.com", true, null, "+AfXPPmsbrRkF+x8RH5i+EofgowXTUkVVImFJw7sy7w=", 1, "intern1" },
                    { 102, new DateTime(2025, 12, 24, 20, 53, 40, 600, DateTimeKind.Utc).AddTicks(7828), "vendor1@example.com", true, null, "Pplu78JXGSTcBXXPb5SF8DnElLUX7/+HLQM0or+ao4k=", 2, "vendor1" }
                });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "VideoUrl",
                value: "https://youtu.be/1");

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "VideoUrl",
                value: "https://youtu.be/2");

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3,
                columns: new[] { "DisplayOrder", "VideoUrl" },
                values: new object[] { 3, "https://youtu.be/3" });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 4,
                columns: new[] { "DisplayOrder", "VideoUrl" },
                values: new object[] { 4, "https://youtu.be/4" });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 5,
                columns: new[] { "DisplayOrder", "VideoUrl" },
                values: new object[] { 5, "https://youtu.be/5" });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 6,
                columns: new[] { "DisplayOrder", "VideoUrl" },
                values: new object[] { 6, "https://youtu.be/6" });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 7,
                columns: new[] { "Description", "DisplayOrder", "VideoUrl" },
                values: new object[] { "Client & team communication", 7, "https://youtu.be/7" });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 8,
                columns: new[] { "DisplayOrder", "VideoUrl" },
                values: new object[] { 8, "https://youtu.be/8" });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "VideoId", "Category", "Description", "DisplayOrder", "Icon", "IsActive", "RoleType", "Title", "VideoUrl" },
                values: new object[,]
                {
                    { 11, "vendor", "How vendors use the platform", 1, "🏢", true, "vendor", "Vendor Overview", "https://youtu.be/9" },
                    { 12, "vendor", "Post openings + requirements", 2, "📄", true, "vendor", "Adding Requirements", "https://youtu.be/10" },
                    { 13, "vendor", "Allocate work to interns", 3, "🗂️", true, "vendor", "Assign Projects", "https://youtu.be/11" },
                    { 14, "vendor", "Monitor activities & progress", 4, "📊", true, "vendor", "Track Intern Work", "https://youtu.be/12" },
                    { 15, "vendor", "Chat & meetings", 5, "💬", true, "vendor", "Communicate with Interns", "https://youtu.be/13" },
                    { 16, "vendor", "Billing + agreements", 6, "💼", true, "vendor", "Payment & Contracts", "https://youtu.be/14" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 102);

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

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "VideoUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "VideoUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3,
                columns: new[] { "DisplayOrder", "VideoUrl" },
                values: new object[] { 1, null });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 4,
                columns: new[] { "DisplayOrder", "VideoUrl" },
                values: new object[] { 1, null });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 5,
                columns: new[] { "DisplayOrder", "VideoUrl" },
                values: new object[] { 1, null });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 6,
                columns: new[] { "DisplayOrder", "VideoUrl" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 7,
                columns: new[] { "Description", "DisplayOrder", "VideoUrl" },
                values: new object[] { "Client & team talks", 1, null });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 8,
                columns: new[] { "DisplayOrder", "VideoUrl" },
                values: new object[] { 2, null });
        }
    }
}
