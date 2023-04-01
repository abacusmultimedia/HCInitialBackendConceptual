using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zero.Persistence.Migrations
{
    public partial class department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INV_MSD_Department",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortAra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true),
                    StatusTypeId = table.Column<int>(type: "int", nullable: true),
                    ActionTypeId = table.Column<int>(type: "int", nullable: true),
                    Action_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprvTypeId = table.Column<int>(type: "int", nullable: true),
                    ApprvUserId = table.Column<int>(type: "int", nullable: true),
                    ApprvDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_MSD_Department", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INV_MSD_Department");
        }
    }
}
