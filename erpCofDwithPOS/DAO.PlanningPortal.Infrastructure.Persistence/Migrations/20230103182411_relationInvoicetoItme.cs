using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zero.Persistence.Migrations
{
    public partial class relationInvoicetoItme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SD_POS_InvoiceDetail_ItemID",
                table: "SD_POS_InvoiceDetail",
                column: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_SD_POS_InvoiceDetail_INV_MSD_SubItemCode_ItemID",
                table: "SD_POS_InvoiceDetail",
                column: "ItemID",
                principalTable: "INV_MSD_SubItemCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SD_POS_InvoiceDetail_INV_MSD_SubItemCode_ItemID",
                table: "SD_POS_InvoiceDetail");

            migrationBuilder.DropIndex(
                name: "IX_SD_POS_InvoiceDetail_ItemID",
                table: "SD_POS_InvoiceDetail");
        }
    }
}
