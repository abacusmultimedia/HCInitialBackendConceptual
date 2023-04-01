using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zero.Persistence.Migrations
{
    public partial class categoryRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_INV_MSD_Item_CatId",
                table: "INV_MSD_Item",
                column: "CatId");

            migrationBuilder.AddForeignKey(
                name: "FK_INV_MSD_Item_INV_MSD_Category_CatId",
                table: "INV_MSD_Item",
                column: "CatId",
                principalTable: "INV_MSD_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INV_MSD_Item_INV_MSD_Category_CatId",
                table: "INV_MSD_Item");

            migrationBuilder.DropIndex(
                name: "IX_INV_MSD_Item_CatId",
                table: "INV_MSD_Item");
        }
    }
}
