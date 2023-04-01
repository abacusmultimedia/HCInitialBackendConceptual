using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zero.Persistence.Migrations
{
    public partial class newfieldsact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "SD_POS_InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "EpAmount",
                table: "SD_POS_InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "SD_POS_InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "SD_POS_InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "SD_POS_InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "SD_POS_InvoiceDetail");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "INV_MSD_SubItemCode",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EpAmount",
                table: "INV_MSD_SubItemCode",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "INV_MSD_SubItemCode",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Stock",
                table: "INV_MSD_SubItemCode",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "INV_MSD_SubItemCode",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Unit",
                table: "INV_MSD_SubItemCode",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "INV_MSD_Item",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "INV_MSD_SubItemCode");

            migrationBuilder.DropColumn(
                name: "EpAmount",
                table: "INV_MSD_SubItemCode");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "INV_MSD_SubItemCode");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "INV_MSD_SubItemCode");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "INV_MSD_SubItemCode");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "INV_MSD_SubItemCode");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "INV_MSD_Item");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "SD_POS_InvoiceDetail",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EpAmount",
                table: "SD_POS_InvoiceDetail",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "SD_POS_InvoiceDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Stock",
                table: "SD_POS_InvoiceDetail",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "SD_POS_InvoiceDetail",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Unit",
                table: "SD_POS_InvoiceDetail",
                type: "decimal(18,6)",
                nullable: true);
        }
    }
}
