using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addUniqueIndexMeetings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Meetings_TherapistId_PatientId",
                table: "Meetings");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_TherapistId_PatientId_startDate",
                table: "Meetings",
                columns: new[] { "TherapistId", "PatientId", "startDate" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Meetings_TherapistId_PatientId_startDate",
                table: "Meetings");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_TherapistId_PatientId",
                table: "Meetings",
                columns: new[] { "TherapistId", "PatientId" },
                unique: true);
        }
    }
}
