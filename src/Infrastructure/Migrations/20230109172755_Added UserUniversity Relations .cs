using Microsoft.EntityFrameworkCore.Migrations;

namespace SSDB.Infrastructure.Migrations
{
    public partial class AddedUserUniversityRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_UniversityId",
                schema: "Identity",
                table: "Users",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Universities_UniversityId",
                schema: "Identity",
                table: "Users",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Universities_UniversityId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UniversityId",
                schema: "Identity",
                table: "Users");
        }
    }
}
