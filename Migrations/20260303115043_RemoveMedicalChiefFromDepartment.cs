using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopitalMvcSqlite.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMedicalChiefFromDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalChief",
                table: "Departments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicalChief",
                table: "Departments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
