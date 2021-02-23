using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KoreFlex.Migrations
{
    public partial class addRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TherapistHoursWorking_Id",
                table: "TherapistHoursWorking");

            migrationBuilder.RenameColumn(
                name: "longMeetMin",
                table: "PatientMeetings",
                newName: "MeetLenghtMin");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "PatientMeetings",
                newName: "startDate");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lengthInMin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomHours_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomHours_RoomId",
                table: "RoomHours",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomHours");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "PatientMeetings",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "MeetLenghtMin",
                table: "PatientMeetings",
                newName: "longMeetMin");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistHoursWorking_Id",
                table: "TherapistHoursWorking",
                column: "Id");
        }
    }
}
