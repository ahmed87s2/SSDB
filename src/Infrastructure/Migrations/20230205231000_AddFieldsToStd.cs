using Microsoft.EntityFrameworkCore.Migrations;

namespace SSDB.Infrastructure.Migrations
{
    public partial class AddFieldsToStd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddmissionNo",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StudyFees",
                table: "Students",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RegistrationFees",
                table: "Batches",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "StudyFees",
                table: "Batches",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddmissionNo",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudyFees",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RegistrationFees",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "StudyFees",
                table: "Batches");
        }
    }
}
