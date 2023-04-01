using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Application.Shared.Models.POS
{
    public class POSInvoiceDocDto
    {
        public long DocumentNo { get; set; }

        public Nullable<short> CompanyID { get; set; }
        public Nullable<short> BranchID { get; set; }

        public Nullable<System.DateTime> DocumentDate { get; set; }
        public Nullable<byte> DocumentTypeID { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string InvoiceTypeID { get; set; }
        public string AccountNo { get; set; }
        public Nullable<decimal> Total { get; set; }
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
        public Nullable<int> Items { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<int> CounterNo { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<byte> ApproveID { get; set; }
        public Nullable<short> ActionUserID { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }
        public Nullable<byte> ActionTypeId { get; set; }
        public Nullable<byte> ExpTypeID { get; set; }
        public Nullable<int> EmpId { get; set; }

        public List<POS_InvoiceDetailDTO> POS_InvoiceDetail { get; set; }

    }


    public class POS_InvoiceDetailDTO
    {
        public long Id { get; set; } 
        public Nullable<long> ItemID { get; set; }
        public string SubitemCode { get; set; }
        public Nullable<short> Seq { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Taxable { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> AvgCost { get; set; }
        public string Status { get; set; }
        public string ShortEng { get; set; }

    }
}
