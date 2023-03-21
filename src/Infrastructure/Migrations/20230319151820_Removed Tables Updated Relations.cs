using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SSDB.Infrastructure.Migrations
{
    public partial class RemovedTablesUpdatedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Universities_UniversityId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Universities_UniversityId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Universities_UniversityId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Universities_UniversityId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Universities_UniversityId",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Universities_UniversityId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Addmissions_AddmissionId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Degrees_DegreeId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Specializations_SpecializationId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsRegistrationInfo_Universities_UniversityId",
                table: "StudentsRegistrationInfo");

            migrationBuilder.DropTable(
                name: "Addmissions");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_StudentsRegistrationInfo_UniversityId",
                table: "StudentsRegistrationInfo");

            migrationBuilder.DropIndex(
                name: "IX_Students_AddmissionId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DegreeId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SpecializationId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_UniversityId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Programs_UniversityId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UniversityId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_UniversityId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_UniversityId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Batches_UniversityId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "OrderColumn",
                table: "Fuculties");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Fuculties");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Adjust",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Batches");

            migrationBuilder.RenameColumn(
                name: "AddmissionId",
                table: "Students",
                newName: "AddmissionType");

            migrationBuilder.AddColumn<int>(
                name: "BatchId",
                table: "Semesters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Programs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Batches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_BatchId",
                table: "Semesters",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_DepartmentId",
                table: "Programs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ProgramId",
                table: "Batches",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Programs_ProgramId",
                table: "Batches",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Departments_DepartmentId",
                table: "Programs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Batches_BatchId",
                table: "Semesters",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Programs_ProgramId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Departments_DepartmentId",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Batches_BatchId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_BatchId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Programs_DepartmentId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Batches_ProgramId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Batches");

            migrationBuilder.RenameColumn(
                name: "AddmissionType",
                table: "Students",
                newName: "AddmissionId");

            migrationBuilder.AddColumn<string>(
                name: "FacultyId",
                table: "Semesters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "Programs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderColumn",
                table: "Fuculties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Fuculties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Adjust",
                table: "Currencies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "Batches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Addmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addmissions_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specializations_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsRegistrationInfo_UniversityId",
                table: "StudentsRegistrationInfo",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AddmissionId",
                table: "Students",
                column: "AddmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DegreeId",
                table: "Students",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SpecializationId",
                table: "Students",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_UniversityId",
                table: "Semesters",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_UniversityId",
                table: "Programs",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UniversityId",
                table: "Payments",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_UniversityId",
                table: "Departments",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_UniversityId",
                table: "Currencies",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_UniversityId",
                table: "Batches",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addmissions_UniversityId",
                table: "Addmissions",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_UniversityId",
                table: "Specializations",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Universities_UniversityId",
                table: "Batches",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Universities_UniversityId",
                table: "Currencies",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Universities_UniversityId",
                table: "Departments",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Universities_UniversityId",
                table: "Payments",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Universities_UniversityId",
                table: "Programs",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Universities_UniversityId",
                table: "Semesters",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Addmissions_AddmissionId",
                table: "Students",
                column: "AddmissionId",
                principalTable: "Addmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Degrees_DegreeId",
                table: "Students",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Specializations_SpecializationId",
                table: "Students",
                column: "SpecializationId",
                principalTable: "Specializations",
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
    }
}
