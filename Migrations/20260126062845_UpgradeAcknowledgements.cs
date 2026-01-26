using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoundersDesk.Migrations
{
    /// <inheritdoc />
    public partial class UpgradeAcknowledgements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "ResourceAcknowledgements");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ResourceAcknowledgements",
                newName: "DocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "ResourceAcknowledgements",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "ResourceType",
                table: "ResourceAcknowledgements",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
