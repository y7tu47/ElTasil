using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElTasilAcademy.Migrations
{
    /// <inheritdoc />
    public partial class fkUpdateMigration_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeworkId",
                table: "HomeworkAnswer");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lesson",
                newName: "LessonId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "HomeworkAnswer",
                newName: "HomeworkAnswerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Homework",
                newName: "HomeworkId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Course",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BookCategory",
                newName: "BookCategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Book",
                newName: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Lesson",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "HomeworkAnswerId",
                table: "HomeworkAnswer",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "HomeworkId",
                table: "Homework",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Course",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BookCategoryId",
                table: "BookCategory",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Book",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "HomeworkId",
                table: "HomeworkAnswer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
