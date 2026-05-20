using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcCore_employeeProject.Migrations
{
    /// <inheritdoc />
    public partial class year : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassingYear",
                table: "AcademicDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassingYear",
                table: "AcademicDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
