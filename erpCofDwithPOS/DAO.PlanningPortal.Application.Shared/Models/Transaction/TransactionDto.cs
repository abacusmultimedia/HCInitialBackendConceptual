using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.TransactionDTo
{


    public class TransactionDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public List<ChildTransactionDto> Transaction { get; set; }
        public int TransactionType { get; set; }
        public List<string> FileList { get; set; }
        public string CreatedByID { get; set; }
    }


    public class ChildTransactionDto
    {
        public long Id { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public long? ItemBatchId { get; set; }
        public long CostCenterId { get; set; }
        public long LedgerId { get; set; }
        public bool isDr { get; set; }
        public string BatchTitle { get; set; }
        public string LedgerTitle { get; set; }
        public string CostCenterTitle { get; set; }
        public string TypeTitle { get; set; }
        public string TypeID { get; set; }
        public List<string> FileList { get; set; }
        public string CreatedByID { get; set; }
        public string CreatedOn { get; set; }
    }

    public class UpDateChildAmountOnly
    {
        public long ParentID { get; set; }
        public double Amount { get; set; }
    }


    public class RemoteValidationItemTransaction
    {
        public long DocumentId { get; set; }
        public long ItemId { get; set; }
    }


}
