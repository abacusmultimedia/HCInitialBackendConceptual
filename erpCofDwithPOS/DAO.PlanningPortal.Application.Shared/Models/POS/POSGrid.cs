using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.POS
{
    public class POSGrid
    {
        public long Id { get; set; }
        public Nullable<int> Seq { get; set; }
        public string SubItemCode { get; set; }
        public string ShortEng { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> EstProfit { get; set; }
        public Nullable<decimal> Selling_Price { get; set; }
        public bool IsPerishable { get; set; }
        public bool isUpdatePariceRequired { get; set; }
        public string ArabicName { get; set; }
    }

    public class POSInvoiceDocReqDto
    {

        public Nullable<decimal> Void { get; set; }
        public Nullable<decimal> Refund { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Round { get; set; }
        public Nullable<decimal> Net { get; set; }
        public Nullable<decimal> Taxable { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> Paid { get; set; }
        public Nullable<decimal> Cash { get; set; }
        public Nullable<decimal> Card { get; set; }
        public Nullable<byte> CardTypeID { get; set; }
        public Nullable<int> CounterNo { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<byte> ApproveID { get; set; }
        public Nullable<short> ActionUserID { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }
        public Nullable<byte> ActionTypeId { get; set; }
        public Nullable<byte> ExpTypeID { get; set; }
        public Nullable<int> EmpId { get; set; }
        public long DocucmentNo { get; set; }
        public long RefDocucmentNo { get; set; }
        public Nullable<System.DateTime> DocumentDate { get; set; }
        public string ModeOfPayment { get; set; }
        public List<POSGrid> POS_InvoiceDetail { get; set; }

    }




    public class POSUserReport
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public int? UserId { get; set; }
        public double Cash_Sale { get; set; }
        public double Cash_SDiscount { get; set; }
        public double Cash_SNet { get; set; }
        public double Card_Sale { get; set; }
        public double Card_Discount { get; set; }
        public double Card_Net { get; set; }

        public double CashNCard_Sale { get; set; }
        public double CashNCard_Discount { get; set; }
        public double CashNCard_Net { get; set; }

        public double Total { get; set; }
        public string InvoicNo { get; set; }
        public double Bkt { get; set; }
        public double Void { get; set; }
        public double Hold { get; set; }
        public bool IsTotal { get; set; }
    }
    //public class POSCategoryReport
    //{
    //    public long Id { get; set; }
    //    public string InvoicNo { get; set; }
    //    public string ItemCode { get; set; }
    //    public double Qty { get; set; }
    //    public double Cash { get; set; }
    //    public double Credit { get; set; }
    //    public double CashCredit { get; set; }
    //    public double SaleRto { get; set; }
    //    public double NetAmount { get; set; }

    //}

    public class POSCategoryReport
    {
        public DateTime? Date { get; set; }
        public long DocNO { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Cash { get; set; }
        public decimal? Credit { get; set; }
        public decimal? CashNCredit { get; set; }
        public decimal? Return { get; set; }

        public decimal? Vat { get; set; }
        public string CateName { get; set; }

    }

    public class POSCategoryReportWithTotal
    {

        public List<CategoryGroup> GroupItems { get; set; }

        public POSCategoryReport Total { get; set; }
        public string Title { get; set; }

    }

    public class CategoryGroup
    {
        public long DocNO { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public List<POSCategoryReport> Items { get; set; }

    }

    public class TotalSalesReportDTO
    {
        public List<POSSalesReportDTO> Items { get; set; }

        public POSSalesReportDTO Total { get; set; }
        public string Title { get; set; }

    }

    public class ReportFilters
    {
        public int CategoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }


    public class POSSalesReportDTO
    {
        public DateTime? Date { get; set; }
        public long DocNO { get; set; }
        public string CustomerName { get; set; }

        public decimal? Cash_Amount { get; set; }
        public decimal? Cash_Discount { get; set; }
        public decimal? Cash_Net { get; set; }
        public decimal? Card_Amount { get; set; }
        public decimal? Card_Discount { get; set; }
        public decimal? Card_Net { get; set; }

        public decimal? CashNCard_Amount { get; set; }
        public decimal? CashNCard_Discount { get; set; }
        public decimal? CashNCard_Net { get; set; }

        public decimal? Total { get; set; }

    }
    public class POSSalesResonseDTO
    {
        public string DocNumber { get; set; }
        public string QRString { get; set; }
    }


}
