using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.Inventory
{
    public class BatchDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long ParentID { get; set; }
        public double OpeningQty { get; set; }
        public double OpeningRate { get; set; }
        public string ParentName { get; set; }
        public string AssignedName { get; set; }
        public long ContractId { get; set; }
    }

    public class AssignBatchtoAgentDTO
    {
        public long Id { get; set; }
        public int AssignedTo { get; set; }
    }

}