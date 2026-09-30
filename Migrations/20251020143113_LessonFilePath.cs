using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElTasilAcademy.Migrations
{
    /// <inheritdoc />
    public partial class LessonFilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Lesson",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Lesson");
        }
    }
}
