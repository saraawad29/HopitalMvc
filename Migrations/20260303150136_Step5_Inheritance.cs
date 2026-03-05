using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopitalMvcSqlite.Migrations
{
    /// <inheritdoc />
    public partial class Step5_Inheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Doctors_DoctorId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Patients_PatientId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Doctors_MedicalChiefId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatients_Doctors_DoctorId",
                table: "DoctorPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatients_Patients_PatientId",
                table: "DoctorPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Departments_DepartmentId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "Person");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_FileNumber",
                table: "Person",
                newName: "IX_Person_FileNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_Email",
                table: "Person",
                newName: "IX_Person_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_DepartmentId",
                table: "Person",
                newName: "IX_Person_DepartmentId");

            migrationBuilder.AlterColumn<string>(
                name: "FileNumber",
                table: "Person",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Person",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Person",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Person",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Doctor_DepartmentId",
                table: "Person",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                table: "Person",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonType",
                table: "Person",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Person",
                type: "TEXT",
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
                name: "IX_Person_LicenseNumber",
                table: "Person",
                column: "LicenseNumber",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IX_Person_LicenseNumber",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Doctor_DepartmentId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Patients");

            migrationBuilder.RenameIndex(
                name: "IX_Person_FileNumber",
                table: "Patients",
                newName: "IX_Patients_FileNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Person_Email",
                table: "Patients",
                newName: "IX_Patients_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Person_DepartmentId",
                table: "Patients",
                newName: "IX_Patients_DepartmentId");

            migrationBuilder.AlterColumn<string>(
                name: "FileNumber",
                table: "Patients",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Patients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Patients",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Patients",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    LicenseNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Specialty = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_LicenseNumber",
                table: "Doctors",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Doctors_DoctorId",
                table: "Consultations",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Patients_PatientId",
                table: "Consultations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Doctors_MedicalChiefId",
                table: "Departments",
                column: "MedicalChiefId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatients_Doctors_DoctorId",
                table: "DoctorPatients",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatients_Patients_PatientId",
                table: "DoctorPatients",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Departments_DepartmentId",
                table: "Patients",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
