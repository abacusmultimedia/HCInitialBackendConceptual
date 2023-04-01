using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.Dashboard
{
    public class WidgetDto
    {
        public string Title { get; set; }
        public string  Description { get; set; }
        public string Value { get; set; }
        public string StylingClass { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
    }
}
