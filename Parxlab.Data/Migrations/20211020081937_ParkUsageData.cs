using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Parxlab.Data.Migrations
{
    public partial class ParkUsageData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Park_SensorId",
                schema: "dbo",
                table: "Park");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LeaveTime",
                schema: "dbo",
                table: "Reserved",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "Park",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ParkUsage",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlateNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getDate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getDate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkUsage_Client",
                        column: x => x.ClientId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParkUsage_Park",
                        column: x => x.ParkId,
                        principalSchema: "dbo",
                        principalTable: "Park",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Park_SensorId",
                schema: "dbo",
                table: "Park",
                column: "SensorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkUsage_ClientId",
                schema: "dbo",
                table: "ParkUsage",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkUsage_ParkId",
                schema: "dbo",
                table: "ParkUsage",
                column: "ParkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkUsage",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Park_SensorId",
                schema: "dbo",
                table: "Park");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LeaveTime",
                schema: "dbo",
                table: "Reserved",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "Park",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Park_SensorId",
                schema: "dbo",
                table: "Park",
                column: "SensorId");
        }
    }
}
