using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SSDB.Infrastructure.Migrations
{
    public partial class AddFieldsInStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameE",
                table: "Students",
                newName: "ThirdNameE");

            migrationBuilder.RenameColumn(
                name: "NameA",
                table: "Students",
                newName: "ThirdNameA");

            migrationBuilder.AddColumn<int>(
                name: "DegreeId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameA",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameE",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FourthNameA",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FourthNameE",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNo",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeatNo",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondNameA",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondNameE",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_DegreeId",
                table: "Students",
                column: "DegreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Degrees_DegreeId",
                table: "Students",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Degrees_DegreeId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropIndex(
                name: "IX_Students_DegreeId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DegreeId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FirstNameA",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FirstNameE",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FourthNameA",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FourthNameE",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IdentityNo",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SeatNo",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SecondNameA",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SecondNameE",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "ThirdNameE",
                table: "Students",
                newName: "NameE");

            migrationBuilder.RenameColumn(
                name: "ThirdNameA",
                table: "Students",
                newName: "NameA");
        }
    }
}
