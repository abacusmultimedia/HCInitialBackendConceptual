using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.GDPRAccess
{
    public class LookupDto
    {
        public string Key { get; set; }
        public string  Title { get; set; }
    }

    public class ItemExistValidationRequestDto {
        public long Id { get; set; }
        public string Barcode { get; set; }
    }

}
