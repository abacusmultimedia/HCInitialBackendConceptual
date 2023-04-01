using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zero.Persistence.Migrations
{
    public partial class paymentMethodAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModeOfPayment",
                table: "SD_POS_Invoice_Template",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModeOfPayment",
                table: "SD_POS_Invoice",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModeOfPayment",
                table: "SD_POS_Invoice_Template");

            migrationBuilder.DropColumn(
                name: "ModeOfPayment",
                table: "SD_POS_Invoice");
        }
    }
}
