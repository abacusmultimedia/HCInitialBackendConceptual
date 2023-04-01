using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zero.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLogDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemKeyword = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Template = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADM_ACC_Bank_Master",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortAra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Addres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Landmark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusTypeId = table.Column<int>(type: "int", nullable: true),
                    ActionUserId = table.Column<int>(type: "int", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprvTypeId = table.Column<int>(type: "int", nullable: true),
                    ApprvUserId = table.Column<int>(type: "int", nullable: true),
                    ApprvDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADM_ACC_Bank_Master", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrachBalances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchID = table.Column<long>(type: "bigint", nullable: false),
                    LedgerID = table.Column<long>(type: "bigint", nullable: false),
                    Opening = table.Column<double>(type: "float", nullable: false),
                    OpeningDate = table.Column<double>(type: "float", nullable: false),
                    MyProperty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrachBalances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INV_MSD_Brand",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortAra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    StatusTypeId = table.Column<int>(type: "int", nullable: false),
                    ActionTypeId = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprvTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprvUserId = table.Column<int>(type: "int", nullable: false),
                    ApprvDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_MSD_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INV_MSD_Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAra = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    ShortAra = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    StatusTypeId = table.Column<int>(type: "int", nullable: false),
                    ActionUserId = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprvTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprvUserId = table.Column<int>(type: "int", nullable: false),
                    ApprvDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_MSD_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INV_MSD_Color",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAra = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    ShortAra = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    StatusTypeId = table.Column<int>(type: "int", nullable: false),
                    ActionUserId = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprvTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprvUserId = table.Column<int>(type: "int", nullable: false),
                    ApprvDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_MSD_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INV_MSD_Item",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAra = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    ShortAra = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    IsDeafult = table.Column<bool>(type: "bit", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    PackiseSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExpiration = table.Column<bool>(type: "bit", nullable: true),
                    ExpiryDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReminderDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DeptId = table.Column<int>(type: "int", nullable: true),
                    CatId = table.Column<int>(type: "int", nullable: true),
                    SubCatId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    ModelId = table.Column<int>(type: "int", nullable: true),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    IsPerishable = table.Column<bool>(type: "bit", nullable: true),
                    IsConsignment = table.Column<bool>(type: "bit", nullable: true),
                    ItemTypeID = table.Column<int>(type: "int", nullable: true),
                    StockMethodID = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    TaxPer = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    EstProfitPer = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    IsPrintable = table.Column<bool>(type: "bit", nullable: true),
                    StatusTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastActived = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DraftedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastDelted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastRecall = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionUserId = table.Column<int>(type: "int", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprvTypeId = table.Column<int>(type: "int", nullable: true),
                    ApprvUserId = table.Column<int>(type: "int", nullable: true),
                    ApprvDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_MSD_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INV_MSD_Model",
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
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    StatusTypeId = table.Column<int>(type: "int", nullable: true),
                    ActionTypeId = table.Column<int>(type: "int", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprvTypeId = table.Column<int>(type: "int", nullable: true),
                    ApprovedTypeId = table.Column<int>(type: "int", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_MSD_Model", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INV_MSD_SubItemPrice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    SubItemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AvgPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    EstProfit = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Selling_Price = table.Column<decimal>(type: "decimal(18,6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_MSD_SubItemPrice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemGroup_ItemGroup_ParentID",
                        column: x => x.ParentID,
                        principalTable: "ItemGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LedgerGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerGroup_LedgerGroup_ParentId",
                        column: x => x.ParentId,
                        principalTable: "LedgerGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReasonToAccess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonToAccess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RouteType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SD_POS_Invoice",
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
                    table.PrimaryKey("PK_SD_POS_Invoice", x => x.DocumentNo);
                });

            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseStock",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseID = table.Column<long>(type: "bigint", nullable: false),
                    ItemBatchID = table.Column<long>(type: "bigint", nullable: false),
                    OpeningBalance = table.Column<double>(type: "float", nullable: false),
                    OpeningRate = table.Column<double>(type: "float", nullable: false),
                    InwardQty = table.Column<double>(type: "float", nullable: false),
                    InwardRate = table.Column<double>(type: "float", nullable: false),
                    OutwardQty = table.Column<double>(type: "float", nullable: false),
                    OutwardRate = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeekDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cell = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    NTN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalInfo_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouse_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostCenter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenter_CostCategory_CostCategoryId",
                        column: x => x.CostCategoryId,
                        principalTable: "CostCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INV_MSD_SubItemCode",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seq = table.Column<int>(type: "int", nullable: true),
                    SubItemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitID = table.Column<int>(type: "int", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CostPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AvgPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    EstProfit = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Selling_Price = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ItemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_MSD_SubItemCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INV_MSD_SubItemCode_INV_MSD_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "INV_MSD_Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descriptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_ItemGroup_ParentID",
                        column: x => x.ParentID,
                        principalTable: "ItemGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<byte>(type: "tinyint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translation_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SD_POS_InvoiceDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<short>(type: "smallint", nullable: true),
                    BranchID = table.Column<short>(type: "smallint", nullable: true),
                    DocumentNo = table.Column<long>(type: "bigint", nullable: true),
                    ItemID = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_SD_POS_InvoiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SD_POS_InvoiceDetail_SD_POS_Invoice_SD_POS_InvoiceDocumentNo",
                        column: x => x.SD_POS_InvoiceDocumentNo,
                        principalTable: "SD_POS_Invoice",
                        principalColumn: "DocumentNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationUnit_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceWorker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceWorker_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    OrganizationUnitId = table.Column<int>(type: "int", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configuration_OrganizationUnit_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "OrganizationUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Configuration_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DraftPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    OrganizationUnitId = table.Column<int>(type: "int", nullable: false),
                    WeekDayId = table.Column<int>(type: "int", nullable: true),
                    DailyPlanDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PlanType = table.Column<byte>(type: "tinyint", nullable: false),
                    RequestedBy = table.Column<int>(type: "int", nullable: false),
                    RequestedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftPlan_OrganizationUnit_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "OrganizationUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftPlan_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftPlan_WeekDays_WeekDayId",
                        column: x => x.WeekDayId,
                        principalTable: "WeekDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdeningGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdeningGroupName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    OrganizationUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdeningGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdeningGroup_OrganizationUnit_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "OrganizationUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdeningGroup_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    OrganizationUnitId = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_OrganizationUnit_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "OrganizationUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNR = table.Column<int>(type: "int", nullable: false),
                    Mail = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    ALB = table.Column<double>(type: "float", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    OrganizationUnitId = table.Column<int>(type: "int", nullable: false),
                    RouteTypeId = table.Column<int>(type: "int", nullable: false),
                    RouteSpeed = table.Column<float>(type: "real", nullable: false),
                    RouteName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    RouteLength = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "DateTime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "DateTime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ServiceWorkerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Route_OrganizationUnit_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "OrganizationUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Route_RouteType_RouteTypeId",
                        column: x => x.RouteTypeId,
                        principalTable: "RouteType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Route_ServiceWorker_ServiceWorkerId",
                        column: x => x.ServiceWorkerId,
                        principalTable: "ServiceWorker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Route_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlternativeServiceWorkersforOrdeningGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdeningGroupId = table.Column<int>(type: "int", nullable: false),
                    ServiceWorkerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternativeServiceWorkersforOrdeningGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternativeServiceWorkersforOrdeningGroup_OrdeningGroup_OrdeningGroupId",
                        column: x => x.OrdeningGroupId,
                        principalTable: "OrdeningGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlternativeServiceWorkersforOrdeningGroup_ServiceWorker_ServiceWorkerId",
                        column: x => x.ServiceWorkerId,
                        principalTable: "ServiceWorker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(550)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Userkey = table.Column<int>(type: "int", nullable: false),
                    ActivityLogTypekey = table.Column<int>(type: "int", nullable: false),
                    ActivityLogDetailkey = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLog_ActivityLogDetail_ActivityLogDetailkey",
                        column: x => x.ActivityLogDetailkey,
                        principalTable: "ActivityLogDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityLog_ActivityLogType_ActivityLogTypekey",
                        column: x => x.ActivityLogTypekey,
                        principalTable: "ActivityLogType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityLog_Users_Userkey",
                        column: x => x.Userkey,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DataAccessRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccessDuration = table.Column<int>(type: "int", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemID = table.Column<int>(type: "int", nullable: false),
                    Environment = table.Column<int>(type: "int", nullable: false),
                    ReasonToAccessID = table.Column<int>(type: "int", nullable: false),
                    ReasonInDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewedCustomerData = table.Column<bool>(type: "bit", nullable: false),
                    AdminBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataAccessRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataAccessRequest_ReasonToAccess_ReasonToAccessID",
                        column: x => x.ReasonToAccessID,
                        principalTable: "ReasonToAccess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataAccessRequest_Systems_SystemID",
                        column: x => x.SystemID,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataAccessRequest_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemBatch",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentID = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemBatch_Item_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemBatch_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ledger",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nature = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Userkey = table.Column<int>(type: "int", nullable: true),
                    PersonalInfoID = table.Column<long>(type: "bigint", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ledger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ledger_LedgerGroup_ParentId",
                        column: x => x.ParentId,
                        principalTable: "LedgerGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ledger_PersonalInfo_PersonalInfoID",
                        column: x => x.PersonalInfoID,
                        principalTable: "PersonalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ledger_Users_Userkey",
                        column: x => x.Userkey,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParentTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentTransaction_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOuMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    OrganizationUnitId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOuMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOuMapping_OrganizationUnit_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "OrganizationUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserOuMapping_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserOuMapping_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasePlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceWorkerId = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    WeekDayId = table.Column<int>(type: "int", nullable: false),
                    OrdeningGroupId = table.Column<int>(type: "int", nullable: false),
                    TransportTypeId = table.Column<int>(type: "int", nullable: false),
                    OrdeningNo = table.Column<int>(type: "int", nullable: false),
                    PayforOwnVehicle = table.Column<bool>(type: "bit", nullable: false),
                    Return = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasePlan_OrdeningGroup_OrdeningGroupId",
                        column: x => x.OrdeningGroupId,
                        principalTable: "OrdeningGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasePlan_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasePlan_ServiceWorker_ServiceWorkerId",
                        column: x => x.ServiceWorkerId,
                        principalTable: "ServiceWorker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasePlan_TransportType_TransportTypeId",
                        column: x => x.TransportTypeId,
                        principalTable: "TransportType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasePlan_WeekDays_WeekDayId",
                        column: x => x.WeekDayId,
                        principalTable: "WeekDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceWorkerId = table.Column<int>(type: "int", nullable: false),
                    TransportTypeId = table.Column<int>(type: "int", nullable: false),
                    OrdeningGroupId = table.Column<int>(type: "int", nullable: false),
                    OrdeningNo = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: true),
                    PayforOwnVehicle = table.Column<bool>(type: "bit", nullable: false),
                    Return = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPlan_OrdeningGroup_OrdeningGroupId",
                        column: x => x.OrdeningGroupId,
                        principalTable: "OrdeningGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyPlan_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyPlan_ServiceWorker_ServiceWorkerId",
                        column: x => x.ServiceWorkerId,
                        principalTable: "ServiceWorker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyPlan_TransportType_TransportTypeId",
                        column: x => x.TransportTypeId,
                        principalTable: "TransportType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentAmount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityAmount = table.Column<double>(type: "float", nullable: false),
                    DueDate = table.Column<int>(type: "int", nullable: false),
                    Frequancy = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    NextDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemBatchId = table.Column<long>(type: "bigint", nullable: true),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    LedgerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_ItemBatch_ItemBatchId",
                        column: x => x.ItemBatchId,
                        principalTable: "ItemBatch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Ledger_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LedgerSMS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LedgerId = table.Column<long>(type: "bigint", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerSMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerSMS_Ledger_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qty = table.Column<double>(type: "float", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDr = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ParentTransactionID = table.Column<long>(type: "bigint", nullable: false),
                    ItemBatchId = table.Column<long>(type: "bigint", nullable: true),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: false),
                    LedgerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_ItemBatch_ItemBatchId",
                        column: x => x.ItemBatchId,
                        principalTable: "ItemBatch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Ledger_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_ParentTransaction_ParentTransactionID",
                        column: x => x.ParentTransactionID,
                        principalTable: "ParentTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionsAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bucket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentTransactionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionsAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionsAttachments_ParentTransaction_ParentTransactionId",
                        column: x => x.ParentTransactionId,
                        principalTable: "ParentTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_ActivityLogDetailkey",
                table: "ActivityLog",
                column: "ActivityLogDetailkey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_ActivityLogTypekey",
                table: "ActivityLog",
                column: "ActivityLogTypekey");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_Userkey",
                table: "ActivityLog",
                column: "Userkey");

            migrationBuilder.CreateIndex(
                name: "IX_AlternativeServiceWorkersforOrdeningGroup_OrdeningGroupId",
                table: "AlternativeServiceWorkersforOrdeningGroup",
                column: "OrdeningGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternativeServiceWorkersforOrdeningGroup_ServiceWorkerId",
                table: "AlternativeServiceWorkersforOrdeningGroup",
                column: "ServiceWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePlan_OrdeningGroupId",
                table: "BasePlan",
                column: "OrdeningGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePlan_RouteId",
                table: "BasePlan",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePlan_ServiceWorkerId",
                table: "BasePlan",
                column: "ServiceWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePlan_TransportTypeId",
                table: "BasePlan",
                column: "TransportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePlan_WeekDayId",
                table: "BasePlan",
                column: "WeekDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_OrganizationUnitId",
                table: "Configuration",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_TenantId",
                table: "Configuration",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CostCenterId",
                table: "Contract",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CreatedBy",
                table: "Contract",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ItemBatchId",
                table: "Contract",
                column: "ItemBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_LedgerId",
                table: "Contract",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_CostCategoryId",
                table: "CostCenter",
                column: "CostCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlan_OrdeningGroupId",
                table: "DailyPlan",
                column: "OrdeningGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlan_RouteId",
                table: "DailyPlan",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlan_ServiceWorkerId",
                table: "DailyPlan",
                column: "ServiceWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlan_TransportTypeId",
                table: "DailyPlan",
                column: "TransportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DataAccessRequest_ReasonToAccessID",
                table: "DataAccessRequest",
                column: "ReasonToAccessID");

            migrationBuilder.CreateIndex(
                name: "IX_DataAccessRequest_SystemID",
                table: "DataAccessRequest",
                column: "SystemID");

            migrationBuilder.CreateIndex(
                name: "IX_DataAccessRequest_UserId",
                table: "DataAccessRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftPlan_OrganizationUnitId",
                table: "DraftPlan",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftPlan_TenantId",
                table: "DraftPlan",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftPlan_WeekDayId",
                table: "DraftPlan",
                column: "WeekDayId");

            migrationBuilder.CreateIndex(
                name: "IX_INV_MSD_SubItemCode_ItemId",
                table: "INV_MSD_SubItemCode",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ParentID",
                table: "Item",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBatch_ParentID",
                table: "ItemBatch",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBatch_UserId",
                table: "ItemBatch",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroup_ParentID",
                table: "ItemGroup",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Ledger_ParentId",
                table: "Ledger",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ledger_PersonalInfoID",
                table: "Ledger",
                column: "PersonalInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Ledger_Userkey",
                table: "Ledger",
                column: "Userkey");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerGroup_ParentId",
                table: "LedgerGroup",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerSMS_LedgerId",
                table: "LedgerSMS",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdeningGroup_OrganizationUnitId",
                table: "OrdeningGroup",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdeningGroup_TenantId",
                table: "OrdeningGroup",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnit_TenantId",
                table: "OrganizationUnit",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentTransaction_CreatedBy",
                table: "ParentTransaction",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInfo_CityId",
                table: "PersonalInfo",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Route_OrganizationUnitId",
                table: "Route",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_RouteTypeId",
                table: "Route",
                column: "RouteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_ServiceWorkerId",
                table: "Route",
                column: "ServiceWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_TenantId",
                table: "Route",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SD_POS_InvoiceDetail_SD_POS_InvoiceDocumentNo",
                table: "SD_POS_InvoiceDetail",
                column: "SD_POS_InvoiceDocumentNo");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceWorker_TenantId",
                table: "ServiceWorker",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CostCenterId",
                table: "Transaction",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CreatedBy",
                table: "Transaction",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ItemBatchId",
                table: "Transaction",
                column: "ItemBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_LedgerId",
                table: "Transaction",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ParentTransactionID",
                table: "Transaction",
                column: "ParentTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsAttachments_ParentTransactionId",
                table: "TransactionsAttachments",
                column: "ParentTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_LanguageId",
                table: "Translation",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOuMapping_OrganizationUnitId",
                table: "UserOuMapping",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOuMapping_TenantId",
                table: "UserOuMapping",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOuMapping_UserId",
                table: "UserOuMapping",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationUnitId",
                table: "Users",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                table: "Users",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_CityID",
                table: "Warehouse",
                column: "CityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLog");

            migrationBuilder.DropTable(
                name: "ActivityType");

            migrationBuilder.DropTable(
                name: "ADM_ACC_Bank_Master");

            migrationBuilder.DropTable(
                name: "AlternativeServiceWorkersforOrdeningGroup");

            migrationBuilder.DropTable(
                name: "BasePlan");

            migrationBuilder.DropTable(
                name: "BrachBalances");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Configuration");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "DailyPlan");

            migrationBuilder.DropTable(
                name: "DataAccessRequest");

            migrationBuilder.DropTable(
                name: "DraftPlan");

            migrationBuilder.DropTable(
                name: "INV_MSD_Brand");

            migrationBuilder.DropTable(
                name: "INV_MSD_Category");

            migrationBuilder.DropTable(
                name: "INV_MSD_Color");

            migrationBuilder.DropTable(
                name: "INV_MSD_Model");

            migrationBuilder.DropTable(
                name: "INV_MSD_SubItemCode");

            migrationBuilder.DropTable(
                name: "INV_MSD_SubItemPrice");

            migrationBuilder.DropTable(
                name: "LedgerSMS");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "SD_POS_InvoiceDetail");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "TransactionsAttachments");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserOuMapping");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "WarehouseStock");

            migrationBuilder.DropTable(
                name: "ActivityLogDetail");

            migrationBuilder.DropTable(
                name: "ActivityLogType");

            migrationBuilder.DropTable(
                name: "OrdeningGroup");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "TransportType");

            migrationBuilder.DropTable(
                name: "ReasonToAccess");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.DropTable(
                name: "WeekDays");

            migrationBuilder.DropTable(
                name: "INV_MSD_Item");

            migrationBuilder.DropTable(
                name: "SD_POS_Invoice");

            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropTable(
                name: "ItemBatch");

            migrationBuilder.DropTable(
                name: "Ledger");

            migrationBuilder.DropTable(
                name: "ParentTransaction");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "RouteType");

            migrationBuilder.DropTable(
                name: "ServiceWorker");

            migrationBuilder.DropTable(
                name: "CostCategory");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "LedgerGroup");

            migrationBuilder.DropTable(
                name: "PersonalInfo");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ItemGroup");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "OrganizationUnit");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
