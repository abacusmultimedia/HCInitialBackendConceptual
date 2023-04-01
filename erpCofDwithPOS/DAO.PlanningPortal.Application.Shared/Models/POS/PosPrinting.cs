using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Application.Shared.Models.POS
{

    public class PosPrinting
    {
        public PrintBilldetails PrintBilldetails { get; set; }
        public DisplayBillDetails DisplayBillDetails { get; set; }

    }
    public class PrintBilldetails
    {

        public List<ItemBilldetails> BillDetails { get; set; }

        public long DocmentNo { get; set; }
        public int? Counter { get; set; }
        public string LoginName { get; set; }
        public string ItemName { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocummentTime { get; set; }
        public string PaymentType { get; set; }
        public decimal? Cash { get; set; }
        public decimal? Card { get; set; }
        public int TotalItems { get; set; }
        public int? TotalQty { get; set; }
        public decimal? Net { get; set; }
        public decimal? Round { get; set; }
        public decimal? Taxable { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Refund { get; set; }
        public decimal? Total { get; set; }
        public decimal? Void { get; set; }
        public double SubTotal { get; set; }
        public string Base64string { get; set; }
        public string CardTypeId { get; set; }
    }
    public class ItemBilldetails
    {
        public long? ItemId { get; set; }
        public string Name { get; set; }
        public string SubItemcode { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? Tax { get; set; }
        public string Status { get; set; }
        public long Id { get; set; }
        public string ItemName { get; set; }
        public decimal? TaxAMount { get; set; }
        public bool IsPerishable { get; set; }
        public bool DuplicateReqried { get; set; }
        public bool IsPerishable13 { get; set; }
    }
    public class DisplayBillDetails
    {
        public long DocumentNo { get; set; }
        public int Items { get; set; }
        public int Qty { get; set; }
        public double Net { get; set; }
        public double Round { get; set; }
        public double Taxable { get; set; }
        public double Paid { get; set; }
        public double Tax { get; set; }
        public double Refund { get; set; }
        public double Total { get; set; }
        public double Void { get; set; }
        public double SubTotal { get; set; }
    }

    public class RecallList
    {
        public DateTime? DateTime { get; set; }
        public long DocNumber { get; set; }
        public long RefDocNumber { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}
