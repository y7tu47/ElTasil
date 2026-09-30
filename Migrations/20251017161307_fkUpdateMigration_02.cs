using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElTasilAcademy.Migrations
{
    /// <inheritdoc />
    public partial class fkUpdateMigration_02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HomeworkAnswer_HomeworkId",
                table: "HomeworkAnswer",
                column: "HomeworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeworkAnswer_Homework_HomeworkId",
                table: "HomeworkAnswer",
                column: "HomeworkId",
                principalTable: "Homework",
                principalColumn: "HomeworkId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeworkAnswer_Homework_HomeworkId",
                table: "HomeworkAnswer");

            migrationBuilder.DropIndex(
                name: "IX_HomeworkAnswer_HomeworkId",
                table: "HomeworkAnswer");
        }
    }
}
