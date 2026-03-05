using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopitalMvcSqlite.Migrations
{
    /// <inheritdoc />
    public partial class Step6_AdvancedModeling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Person_DoctorId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Person_PatientId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Person_MedicalChiefId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatients_Person_DoctorId",
                table: "DoctorPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatients_Person_PatientId",
                table: "DoctorPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Departments_DepartmentId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Departments_Doctor_DepartmentId",
                table: "Person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_Doctor_DepartmentId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_Email",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_FileNumber",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Doctor_DepartmentId",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Staff");

            migrationBuilder.RenameColumn(
                name: "PersonType",
                table: "Staff",
                newName: "StaffType");

            migrationBuilder.RenameColumn(
                name: "FileNumber",
                table: "Staff",
                newName: "Service");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Staff",
                newName: "Grade");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Staff",
                newName: "Function");

            migrationBuilder.RenameIndex(
                name: "IX_Person_LicenseNumber",
                table: "Staff",
                newName: "IX_Staff_LicenseNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Person_DepartmentId",
                table: "Staff",
                newName: "IX_Staff_DepartmentId");

            migrationBuilder.AddColumn<string>(
                name: "Contact_City",
                table: "Departments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contact_Country",
                table: "Departments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact_PostalCode",
                table: "Departments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contact_Street",
                table: "Departments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParentDepartmentId",
                table: "Departments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "Staff",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Staff",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Address_Street = table.Column<string>(type: "TEXT", nullable: false),
                    Address_City = table.Column<string>(type: "TEXT", nullable: false),
                    Address_PostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    Address_Country = table.Column<string>(type: "TEXT", nullable: true),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentDepartmentId",
                table: "Departments",
                column: "ParentDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DepartmentId",
                table: "Patients",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                table: "Patients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_FileNumber",
                table: "Patients",
                column: "FileNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Patients_PatientId",
                table: "Consultations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Staff_DoctorId",
                table: "Consultations",
                column: "DoctorId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Departments_ParentDepartmentId",
                table: "Departments",
                column: "ParentDepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Staff_MedicalChiefId",
                table: "Departments",
                column: "MedicalChiefId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatients_Patients_PatientId",
                table: "DoctorPatients",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatients_Staff_DoctorId",
                table: "DoctorPatients",
                column: "DoctorId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Departments_DepartmentId",
                table: "Staff",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Patients_PatientId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Staff_DoctorId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Departments_ParentDepartmentId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Staff_MedicalChiefId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatients_Patients_PatientId",
                table: "DoctorPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatients_Staff_DoctorId",
                table: "DoctorPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Departments_DepartmentId",
                table: "Staff");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ParentDepartmentId",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Contact_City",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Contact_Country",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Contact_PostalCode",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Contact_Street",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ParentDepartmentId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Staff");

            migrationBuilder.RenameTable(
                name: "Staff",
                newName: "Person");

            migrationBuilder.RenameColumn(
                name: "StaffType",
                table: "Person",
                newName: "PersonType");

            migrationBuilder.RenameColumn(
                name: "Service",
                table: "Person",
                newName: "FileNumber");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Person",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "Function",
                table: "Person",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_LicenseNumber",
                table: "Person",
                newName: "IX_Person_LicenseNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_DepartmentId",
                table: "Person",
                newName: "IX_Person_DepartmentId");

            migrationBuilder.AddColumn<int>(
                name: "Doctor_DepartmentId",
                table: "Person",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Doctor_DepartmentId",
                table: "Person",
                column: "Doctor_DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Email",
                table: "Person",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_FileNumber",
                table: "Person",
                column: "FileNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Person_DoctorId",
                table: "Consultations",
                column: "DoctorId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Person_PatientId",
                table: "Consultations",
                column: "PatientId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Person_MedicalChiefId",
                table: "Departments",
                column: "MedicalChiefId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatients_Person_DoctorId",
                table: "DoctorPatients",
                column: "DoctorId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatients_Person_PatientId",
                table: "DoctorPatients",
                column: "PatientId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Departments_DepartmentId",
                table: "Person",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Departments_Doctor_DepartmentId",
                table: "Person",
                column: "Doctor_DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
