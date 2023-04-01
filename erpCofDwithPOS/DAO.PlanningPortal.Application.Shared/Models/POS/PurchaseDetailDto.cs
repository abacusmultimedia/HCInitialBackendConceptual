using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Application.Shared.Models.POS
{

    public class PurchaseInvoiceDto
    {


        public string DocumentNo { get; set; }
        public DateTime DateEng { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public int InvoiceType { get; set; }
        public int DueDays { get; set; }
        public DateTime PayDate { get; set; }
        public string AccountNumber { get; set; }
        public string Remarks { get; set; }
        public int SupplierId { get; set; }



        public int SuppType { get; set; }
        public string CurrencyCode { get; set; }


        public List<PurchaseInvoiceDetailDto> ItemDetails { get; set; }
        public PuchaseInvoicePricingDetails PuchasePricingDetails { get; set; }
}
    public class PurchaseInvoiceDetailDto
    {
        public long Id { get; set; }
        public Nullable<long> ItemID { get; set; }
        public string SubitemCode { get; set; }
        public string ItemName { get; set; }
        public Nullable<short> Seq { get; set; }
        public Nullable<int> CQty { get; set; }
        public Nullable<int> PQty { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<int> FQty { get; set; }
        public Nullable<decimal> UPrice { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public DateTime EDate { get; set; }
        public string WarningDays { get; set; }

        public Nullable<decimal> VatTax { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }

    public class PurcahseListDto
    {
        public long Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
    }
    public class PurchaseListFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class PuchaseInvoicePricingDetails
    {
        public decimal SalesPrice { get; set; }
        public decimal SalesDiscAmt   { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ProfitAmt { get; set; }
        public decimal MinSalesPrice { get; set; }
        public decimal DiscoutAmount { get; set; }
        public decimal  TransAmount { get; set; }
        public decimal QtyOnHand { get; set; }
        public decimal TotalPurchaseCQty { get; set; }
        public decimal VAT { get; set; }
        public decimal NetAmount { get; set; }

    }
}
