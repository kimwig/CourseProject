using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProjectLiteMK5.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPostMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewContent",
                table: "Posts",
                type: "varchar(8192)",
                maxLength: 8192,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewTitle",
                table: "Posts",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewContent",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "NewTitle",
                table: "Posts");
        }
    }
}
