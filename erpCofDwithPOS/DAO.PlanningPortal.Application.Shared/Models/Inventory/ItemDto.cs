using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.Inventory
{
    public class ItemDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long ParentID { get; set; }
        public string ParentName { get; set; }
        public string Descriptions { get; set; }
        public List<BatchDto> BatchList { get; set; } = new List<BatchDto>();
    }
}
