using Microsoft.EntityFrameworkCore.Migrations;

namespace KoreFlex.Migrations
{
    public partial class addUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Therapists",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Patients",
                newName: "FullName");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Therapists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Therapists",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Patients",
                newName: "Name");
        }
    }
}
