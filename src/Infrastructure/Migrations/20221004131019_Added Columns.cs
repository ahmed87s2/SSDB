using Microsoft.EntityFrameworkCore.Migrations;

namespace SSDB.Infrastructure.Migrations
{
    public partial class AddedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tax",
                table: "Universities",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Students",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Barcode",
                table: "Students",
                newName: "Collage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Universities",
                newName: "Tax");

            migrationBuilder.RenameColumn(
                name: "Collage",
                table: "Students",
                newName: "Barcode");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Students",
                newName: "Rate");
        }
    }
}
