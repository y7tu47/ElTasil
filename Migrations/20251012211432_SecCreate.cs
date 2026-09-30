using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElTasilAcademy.Migrations
{
    /// <inheritdoc />
    public partial class SecCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaModel",
                table: "MediaModel");

            migrationBuilder.RenameTable(
                name: "MediaModel",
                newName: "Lesson");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson");

            migrationBuilder.RenameTable(
                name: "Lesson",
                newName: "MediaModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaModel",
                table: "MediaModel",
                column: "Id");
        }
    }
}
