using Microsoft.EntityFrameworkCore.Migrations;

namespace SSDB.Infrastructure.Migrations
{
    public partial class RemovedTablesUpdatedRelations3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Currencies_UniversityId",
                table: "Currencies",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Universities_UniversityId",
                table: "Currencies",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Universities_UniversityId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_UniversityId",
                table: "Currencies");
        }
    }
}
