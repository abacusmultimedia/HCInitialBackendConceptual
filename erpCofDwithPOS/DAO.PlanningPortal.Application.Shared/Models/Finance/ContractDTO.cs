using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.Finance
{
    public class ContractDTO
    {
        public long? Id { get; set; }

        public double RentAmount { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double SecurityAmount { get; set; }
        public int DueDate { get; set; }
        public int Frequancy { get; set; }

        public long LedgerId { get; set; }
        public long CostCenterId { get; set; }
        public long? ItemBatchId { get; set; }
        public string DescriptionCR { get; set; }
        public string DescriptionDR { get; set; }

        public string VenderTitle { get; set; }
        public string CustomerTitle { get; set; }
        public string ItemTitle { get; set; }
        public bool IsActive { get; set; }
        /// Relations 
    }


    public class RentReceiptByContractDTO
    {
        public long ContractID { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public long? ModeOfPayment { get; set; }
        public IFormFile File { get; set; }


    }
    public class RentReceiptDTO
    {
        public long? Id { get; set; }
        public double? Qty { get; set; }
        public double? Rate { get; set; }
        public double Amount { get; set; }
        public DateTime? Date { get; set; }
        public long ledgerIdCR { get; set; }
        public long ledgerIdDR { get; set; }
        public long CostCenterId { get; set; }
        public long? ItemBatchId { get; set; }
        public string Description { get; set; }
        public string DescriptionCR { get; set; }
        public string DescriptionDR { get; set; }

        public string VenderTitle { get; set; }
        public string CustomerTitle { get; set; }
        public string ItemTitle { get; set; }
        /// Relations 
    }
    public class ContractStatusDto
    {
        public long DocId { get; set; }
        public bool Status { get; set; }
    }
}
