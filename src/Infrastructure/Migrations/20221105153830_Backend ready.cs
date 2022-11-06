using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SSDB.Infrastructure.Migrations
{
    public partial class Backendready : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
               name: "PK_Students",
               table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.AddColumn<string>(
               name: "Id",
               table: "Students",
               type: "nvarchar(50)",
               nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Registrations_RegistrationId1",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_RegistrationId1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RegistrationId1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentStatus",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Registrations");

            migrationBuilder.RenameColumn(
                name: "comments",
                table: "Registrations",
                newName: "Comments");

            migrationBuilder.AlterColumn<bool>(
                name: "NoStudyFees",
                table: "Students",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");


            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Registrations",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_StudentId",
                table: "Registrations",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Students_StudentId",
                table: "Registrations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Students_StudentId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_StudentId",
                table: "Registrations");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "Registrations",
                newName: "comments");

            migrationBuilder.AlterColumn<int>(
                name: "NoStudyFees",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistrationId1",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentStatus",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Students_RegistrationId1",
                table: "Students",
                column: "RegistrationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Registrations_RegistrationId1",
                table: "Students",
                column: "RegistrationId1",
                principalTable: "Registrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
