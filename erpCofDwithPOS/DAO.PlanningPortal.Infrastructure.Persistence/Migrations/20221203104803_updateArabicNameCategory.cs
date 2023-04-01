using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zero.Persistence.Migrations
{
    public partial class updateArabicNameCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "INV_MSD_SubItemCode",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortArb",
                table: "INV_MSD_SubItemCode",
                type: "nvarchar(1000)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_INV_MSD_SubItemCode_CatId",
                table: "INV_MSD_SubItemCode",
                column: "CatId");

            migrationBuilder.AddForeignKey(
                name: "FK_INV_MSD_SubItemCode_INV_MSD_Category_CatId",
                table: "INV_MSD_SubItemCode",
                column: "CatId",
                principalTable: "INV_MSD_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INV_MSD_SubItemCode_INV_MSD_Category_CatId",
                table: "INV_MSD_SubItemCode");

            migrationBuilder.DropIndex(
                name: "IX_INV_MSD_SubItemCode_CatId",
                table: "INV_MSD_SubItemCode");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "INV_MSD_SubItemCode");

            migrationBuilder.DropColumn(
                name: "ShortArb",
                table: "INV_MSD_SubItemCode");
        }
    }
}
