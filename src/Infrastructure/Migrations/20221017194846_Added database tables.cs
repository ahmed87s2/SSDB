using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SSDB.Infrastructure.Migrations
{
    public partial class Addeddatabasetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Universities_UniversityId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ImageDataURL",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "Std_Picture");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Students",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Collage",
                table: "Students",
                newName: "RegType");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Students",
                newName: "ToLocalCurrency");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AddmissionFormNo",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddmissionId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AdvisorId",
                table: "Students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "BatchId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CGPA",
                table: "Students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CertificateType",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "First_semster",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FucultyId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GraduationDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "MedicalFees",
                table: "Students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NameA",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameE",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalityId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NextSemester",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoStudyFees",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Record_Status",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistrationId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegistrationId1",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StdPassword",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentStatus",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "StudyFeesUpdated",
                table: "Students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Addmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addmissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adjust = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fuculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderColumn = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FucultyId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Fuculties_FucultyId",
                        column: x => x.FucultyId,
                        principalTable: "Fuculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    MaxHours = table.Column<double>(type: "float", nullable: false),
                    FacultyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FucultyId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_Fuculties_FucultyId",
                        column: x => x.FucultyId,
                        principalTable: "Fuculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Semesters_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Fees = table.Column<double>(type: "float", nullable: false),
                    StudyFees = table.Column<double>(type: "float", nullable: false),
                    NoStudyFees = table.Column<bool>(type: "bit", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    PaymentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    linkNo = table.Column<int>(type: "int", nullable: false),
                    comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_AddmissionId",
                table: "Students",
                column: "AddmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_BatchId",
                table: "Students",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CurrencyId",
                table: "Students",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FucultyId",
                table: "Students",
                column: "FucultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgramId",
                table: "Students",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_RegistrationId1",
                table: "Students",
                column: "RegistrationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SpecializationId",
                table: "Students",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FucultyId",
                table: "Departments",
                column: "FucultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_CurrencyId",
                table: "Registrations",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_SemesterId",
                table: "Registrations",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_FucultyId",
                table: "Semesters",
                column: "FucultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_ProgramId",
                table: "Semesters",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Addmissions_AddmissionId",
                table: "Students",
                column: "AddmissionId",
                principalTable: "Addmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Batches_BatchId",
                table: "Students",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Currencies_CurrencyId",
                table: "Students",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Fuculties_FucultyId",
                table: "Students",
                column: "FucultyId",
                principalTable: "Fuculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Programs_ProgramId",
                table: "Students",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Registrations_RegistrationId1",
                table: "Students",
                column: "RegistrationId1",
                principalTable: "Registrations",
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
                name: "FK_Students_Universities_UniversityId",
                table: "Students",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Addmissions_AddmissionId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Batches_BatchId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Currencies_CurrencyId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Fuculties_FucultyId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Programs_ProgramId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Registrations_RegistrationId1",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Specializations_SpecializationId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Universities_UniversityId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Addmissions");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Fuculties");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Students_AddmissionId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_BatchId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CurrencyId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FucultyId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ProgramId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_RegistrationId1",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SpecializationId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AddmissionFormNo",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AddmissionId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AdvisorId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CGPA",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CertificateType",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "First_semster",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FucultyId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GraduationDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MedicalFees",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "NameA",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "NameE",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "NextSemester",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "NoStudyFees",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Record_Status",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RegistrationId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RegistrationId1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StdPassword",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentStatus",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudyFeesUpdated",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "ToLocalCurrency",
                table: "Students",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Std_Picture",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Students",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "RegType",
                table: "Students",
                newName: "Collage");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageDataURL",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Universities_UniversityId",
                table: "Students",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
