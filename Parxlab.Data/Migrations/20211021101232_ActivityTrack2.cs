using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Parxlab.Data.Migrations
{
    public partial class ActivityTrack2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLog",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Identity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getDate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getDate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLog_User",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_UserId",
                schema: "dbo",
                table: "ActivityLog",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLog",
                schema: "dbo");
        }
    }
}
