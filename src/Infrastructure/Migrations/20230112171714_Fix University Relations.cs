using Microsoft.EntityFrameworkCore.Migrations;

namespace SSDB.Infrastructure.Migrations
{
    public partial class FixUniversityRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addmissions_Universities_UniversityId",
                table: "Addmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Addmissions_Universities_UniversityId1",
                table: "Addmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Universities_UniversityId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Universities_UniversityId1",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Universities_UniversityId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Universities_UniversityId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Universities_UniversityId1",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Fuculties_Universities_UniversityId",
                table: "Fuculties");

            migrationBuilder.DropForeignKey(
                name: "FK_Fuculties_Universities_UniversityId1",
                table: "Fuculties");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Universities_UniversityId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Universities_UniversityId1",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Universities_UniversityId",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Universities_UniversityId1",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Universities_UniversityId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Universities_UniversityId1",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Universities_UniversityId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Universities_UniversityId1",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_Universities_UniversityId",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_Universities_UniversityId1",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Universities_UniversityId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Universities_UniversityId1",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsRegistrationInfo_Universities_UniversityId",
                table: "StudentsRegistrationInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsRegistrationInfo_Universities_UniversityId1",
                table: "StudentsRegistrationInfo");

            migrationBuilder.DropIndex(
                name: "IX_StudentsRegistrationInfo_UniversityId1",
                table: "StudentsRegistrationInfo");

            migrationBuilder.DropIndex(
                name: "IX_Students_UniversityId1",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_UniversityId1",
                table: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_UniversityId1",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_UniversityId1",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Programs_UniversityId1",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UniversityId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Fuculties_UniversityId1",
                table: "Fuculties");

            migrationBuilder.DropIndex(
                name: "IX_Departments_UniversityId1",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_UniversityId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Batches_UniversityId1",
                table: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Addmissions_UniversityId1",
                table: "Addmissions");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "StudentsRegistrationInfo");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "Fuculties");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "Addmissions");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Specializations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Semesters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Registrations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Programs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Fuculties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Departments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Batches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Addmissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Addmissions_Universities_UniversityId",
                table: "Addmissions",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Universities_UniversityId",
                table: "Batches",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Universities_UniversityId",
                table: "Departments",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fuculties_Universities_UniversityId",
                table: "Fuculties",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Universities_UniversityId",
                table: "Payments",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Universities_UniversityId",
                table: "Programs",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Universities_UniversityId",
                table: "Registrations",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Universities_UniversityId",
                table: "Semesters",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_Universities_UniversityId",
                table: "Specializations",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Universities_UniversityId",
                table: "Students",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsRegistrationInfo_Universities_UniversityId",
                table: "StudentsRegistrationInfo",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addmissions_Universities_UniversityId",
                table: "Addmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Universities_UniversityId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Universities_UniversityId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Fuculties_Universities_UniversityId",
                table: "Fuculties");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Universities_UniversityId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Universities_UniversityId",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Universities_UniversityId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Universities_UniversityId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_Universities_UniversityId",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Universities_UniversityId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsRegistrationInfo_Universities_UniversityId",
                table: "StudentsRegistrationInfo");

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "StudentsRegistrationInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Specializations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "Specializations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "Semesters",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "Registrations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Programs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "Programs",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Fuculties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "Fuculties",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "Currencies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Batches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "Batches",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Addmissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "Addmissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentsRegistrationInfo_UniversityId1",
                table: "StudentsRegistrationInfo",
                column: "UniversityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UniversityId1",
                table: "Students",
                column: "UniversityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_UniversityId1",
                table: "Specializations",
                column: "UniversityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_UniversityId1",
                table: "Semesters",
                column: "UniversityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UniversityId1",
                table: "Registrations",
                column: "UniversityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_UniversityId1",
                table: "Programs",
                column: "UniversityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UniversityId1",
                table: "Payments",
                column: "UniversityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Fuculties_UniversityId1",
                table: "Fuculties",
                column: "UniversityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_UniversityId1",
                table: "Departments",
                column: "UniversityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_UniversityId",
                table: "Currencies",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_UniversityId1",
                table: "Batches",
                column: "UniversityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Addmissions_UniversityId1",
                table: "Addmissions",
                column: "UniversityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Addmissions_Universities_UniversityId",
                table: "Addmissions",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addmissions_Universities_UniversityId1",
                table: "Addmissions",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Universities_UniversityId",
                table: "Batches",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Universities_UniversityId1",
                table: "Batches",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Universities_UniversityId",
                table: "Currencies",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Universities_UniversityId",
                table: "Departments",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Universities_UniversityId1",
                table: "Departments",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fuculties_Universities_UniversityId",
                table: "Fuculties",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fuculties_Universities_UniversityId1",
                table: "Fuculties",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Universities_UniversityId",
                table: "Payments",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Universities_UniversityId1",
                table: "Payments",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Universities_UniversityId",
                table: "Programs",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Universities_UniversityId1",
                table: "Programs",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Universities_UniversityId",
                table: "Registrations",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Universities_UniversityId1",
                table: "Registrations",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Universities_UniversityId",
                table: "Semesters",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Universities_UniversityId1",
                table: "Semesters",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_Universities_UniversityId",
                table: "Specializations",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_Universities_UniversityId1",
                table: "Specializations",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Universities_UniversityId",
                table: "Students",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Universities_UniversityId1",
                table: "Students",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsRegistrationInfo_Universities_UniversityId",
                table: "StudentsRegistrationInfo",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsRegistrationInfo_Universities_UniversityId1",
                table: "StudentsRegistrationInfo",
                column: "UniversityId1",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
