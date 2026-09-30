using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElTasilAcademy.Migrations
{
    /// <inheritdoc />
    public partial class fkUpdateMigration_08 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "B");

            migrationBuilder.DropTable(
                name: "CourseUser");

            migrationBuilder.DropTable(
                name: "A");

            migrationBuilder.DropIndex(
                name: "IX_Homework_LessonId",
                table: "Homework");

            migrationBuilder.CreateIndex(
                name: "IX_Homework_LessonId",
                table: "Homework",
                column: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Homework_LessonId",
                table: "Homework");

            migrationBuilder.CreateTable(
                name: "A",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseUser",
                columns: table => new
                {
                    CoursesCourseId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUser", x => new { x.CoursesCourseId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_CourseUser_Course_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUser_User_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "B",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B", x => x.Id);
                    table.ForeignKey(
                        name: "FK_B_A_AId",
                        column: x => x.AId,
                        principalTable: "A",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Homework_LessonId",
                table: "Homework",
                column: "LessonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_B_AId",
                table: "B",
                column: "AId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUser_UsersUserId",
                table: "CourseUser",
                column: "UsersUserId");
        }
    }
}
