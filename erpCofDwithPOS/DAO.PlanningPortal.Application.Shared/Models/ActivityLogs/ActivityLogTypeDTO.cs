using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.ActivityLogs
{
    public class ActivityLogTypeDTO
    {
        public int Id { get; set; }
        public string SystemKeyword { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Template { get; set; }
        public int GroupId { get; set; }
    }
}
