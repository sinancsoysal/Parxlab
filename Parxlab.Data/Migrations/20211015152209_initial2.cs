using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Parxlab.Data.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "dbo",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                schema: "dbo",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getDate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "JwtId",
                schema: "dbo",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getDate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Park",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SensorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getDate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getDate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Park", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Park_Sensor_SensorId",
                        column: x => x.SensorId,
                        principalSchema: "dbo",
                        principalTable: "Sensor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserved",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlateNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getDate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getDate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserved", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserved_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getDate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getDate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Reserved_ReservedId",
                        column: x => x.ReservedId,
                        principalSchema: "dbo",
                        principalTable: "Reserved",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ReservedId",
                schema: "dbo",
                table: "Invoice",
                column: "ReservedId");

            migrationBuilder.CreateIndex(
                name: "IX_Park_SensorId",
                schema: "dbo",
                table: "Park",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserved_UserId",
                schema: "dbo",
                table: "Reserved",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Park",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Reserved",
                schema: "dbo");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "dbo",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                schema: "dbo",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getDate())");

            migrationBuilder.AlterColumn<string>(
                name: "JwtId",
                schema: "dbo",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getDate())");
        }
    }
}
