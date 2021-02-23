using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KoreFlex.Migrations
{
    public partial class addRoomIdToPMAndDeleteRHAndAddUCM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "RoomId",
                table: "PatientMeetings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "unhandledCancelMeetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TherapistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LengthMeetInMin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unhandledCancelMeetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_unhandledCancelMeetings_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_unhandledCancelMeetings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_unhandledCancelMeetings_Therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "Therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientMeetings_RoomId",
                table: "PatientMeetings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_unhandledCancelMeetings_PatientId",
                table: "unhandledCancelMeetings",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_unhandledCancelMeetings_RoomId",
                table: "unhandledCancelMeetings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_unhandledCancelMeetings_TherapistId",
                table: "unhandledCancelMeetings",
                column: "TherapistId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientMeetings_Rooms_RoomId",
                table: "PatientMeetings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientMeetings_Rooms_RoomId",
                table: "PatientMeetings");

            migrationBuilder.DropTable(
                name: "unhandledCancelMeetings");

            migrationBuilder.DropIndex(
                name: "IX_PatientMeetings_RoomId",
                table: "PatientMeetings");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "PatientMeetings");

            migrationBuilder.CreateTable(
                name: "RoomHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    lengthInMin = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
    }
}
