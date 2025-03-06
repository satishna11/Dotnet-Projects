using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dao.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InfoID",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Student_InfoID",
                table: "Student",
                column: "InfoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_CourseInfo_InfoID",
                table: "Student",
                column: "InfoID",
                principalTable: "CourseInfo",
                principalColumn: "InfoID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_CourseInfo_InfoID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_InfoID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "InfoID",
                table: "Student");
        }
    }
}
