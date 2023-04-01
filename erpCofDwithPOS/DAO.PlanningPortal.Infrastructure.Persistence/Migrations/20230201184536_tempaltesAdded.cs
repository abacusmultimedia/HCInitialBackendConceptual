using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zero.Persistence.Migrations
{
    public partial class tempaltesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SD_POS_Invoice_Template",
                columns: table => new
                {
                    DocumentNo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<short>(type: "smallint", nullable: true),
                    BranchID = table.Column<short>(type: "smallint", nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentTypeID = table.Column<byte>(type: "tinyint", nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceTypeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Void = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Refund = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Round = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Net = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Taxable = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Paid = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Cash = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Card = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    CardTypeID = table.Column<byte>(type: "tinyint", nullable: true),
                    Items = table.Column<int>(type: "int", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    CounterNo = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: true),
                    ApproveID = table.Column<byte>(type: "tinyint", nullable: true),
                    ActionUserID = table.Column<short>(type: "smallint", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionTypeId = table.Column<byte>(type: "tinyint", nullable: true),
                    ExpTypeID = table.Column<byte>(type: "tinyint", nullable: true),
                    EmpId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SD_POS_Invoice_Template", x => x.DocumentNo);
                });

            migrationBuilder.CreateTable(
                name: "SD_POS_InvoiceDetail_Template",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<short>(type: "smallint", nullable: true),
                    BranchID = table.Column<short>(type: "smallint", nullable: true),
                    DocumentNo = table.Column<long>(type: "bigint", nullable: true),
                    ItemID = table.Column<long>(type: "bigint", nullable: false),
                    SubitemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seq = table.Column<short>(type: "smallint", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Taxable = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AvgCost = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SD_POS_InvoiceDocumentNo = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SD_POS_InvoiceDetail_Template", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SD_POS_InvoiceDetail_Template_INV_MSD_SubItemCode_ItemID",
                        column: x => x.ItemID,
                        principalTable: "INV_MSD_SubItemCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SD_POS_InvoiceDetail_Template_SD_POS_Invoice_Template_SD_POS_InvoiceDocumentNo",
                        column: x => x.SD_POS_InvoiceDocumentNo,
                        principalTable: "SD_POS_Invoice_Template",
                        principalColumn: "DocumentNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SD_POS_InvoiceDetail_Template_ItemID",
                table: "SD_POS_InvoiceDetail_Template",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_SD_POS_InvoiceDetail_Template_SD_POS_InvoiceDocumentNo",
                table: "SD_POS_InvoiceDetail_Template",
                column: "SD_POS_InvoiceDocumentNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SD_POS_InvoiceDetail_Template");

            migrationBuilder.DropTable(
                name: "SD_POS_Invoice_Template");
        }
    }
}
