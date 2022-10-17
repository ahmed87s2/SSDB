using Microsoft.EntityFrameworkCore.Migrations;

namespace SSDB.Infrastructure.Migrations
{
    public partial class AddedstudentSemesterrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Fuculties_FucultyId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Programs_ProgramId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_FucultyId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_ProgramId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "FucultyId",
                table: "Semesters");

            migrationBuilder.RenameColumn(
                name: "NextSemester",
                table: "Students",
                newName: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SemesterId",
                table: "Students",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SemesterId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "Students",
                newName: "NextSemester");

            migrationBuilder.AddColumn<int>(
                name: "FucultyId",
                table: "Semesters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_FucultyId",
                table: "Semesters",
                column: "FucultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_ProgramId",
                table: "Semesters",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Fuculties_FucultyId",
                table: "Semesters",
                column: "FucultyId",
                principalTable: "Fuculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Programs_ProgramId",
                table: "Semesters",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
