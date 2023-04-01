using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.Reporting
{
    public class OutstandingRentDto
    {
        public long batchId { get; set; }
        public string BatchName { get; set; }
        public string ItemName { get; set; }
        public string CustomerName { get; set; }
        public double CrAmount { get; set; }
        public double DrAmount { get; set; }
        public long ContractID { get; set; }

    }
}
