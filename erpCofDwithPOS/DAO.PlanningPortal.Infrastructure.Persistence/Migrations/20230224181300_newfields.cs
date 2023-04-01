using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zero.Persistence.Migrations
{
    public partial class newfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
