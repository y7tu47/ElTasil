using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElTasilAcademy.Migrations
{
    /// <inheritdoc />
    public partial class fkUpdateMigration_03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_CourseId",
                table: "Lesson",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkAnswer_UserId",
                table: "HomeworkAnswer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Homework_LessonId",
                table: "Homework",
                column: "LessonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_UserId",
                table: "Course",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_BookCategoryId",
                table: "Book",
                column: "BookCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CourseId",
                table: "Book",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookCategory_BookCategoryId",
                table: "Book",
                column: "BookCategoryId",
                principalTable: "BookCategory",
                principalColumn: "BookCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Course_CourseId",
                table: "Book",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_User_UserId",
                table: "Course",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homework_Lesson_LessonId",
                table: "Homework",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeworkAnswer_User_UserId",
                table: "HomeworkAnswer",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Course_CourseId",
                table: "Lesson",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookCategory_BookCategoryId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Course_CourseId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_User_UserId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Homework_Lesson_LessonId",
                table: "Homework");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeworkAnswer_User_UserId",
                table: "HomeworkAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Course_CourseId",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_CourseId",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_HomeworkAnswer_UserId",
                table: "HomeworkAnswer");

            migrationBuilder.DropIndex(
                name: "IX_Homework_LessonId",
                table: "Homework");

            migrationBuilder.DropIndex(
                name: "IX_Course_UserId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Book_BookCategoryId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_CourseId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Course");
        }
    }
}
