using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.Inventory
{
    public class ItemGroupDto
    {
        public long Id { get; set; }
        public string Title { get; set; } 
        public long? ParentID { get; set; }
        public string ParentTitle { get; set; }
    }
}
