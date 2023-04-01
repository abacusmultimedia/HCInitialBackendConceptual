using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.POS
{
    public class ItemPOSDto
    {
        public string ShortEng { get; set; }
        public string SubItemCode { get; set; }
        public Nullable<decimal> Selling_Price { get; set; }
    }
    public class ItemPOSLookupSearchReq
    {
        public string ShortEng { get; set; }
    }

    public class ItemMasterReqLazyLoad
    {
        public string ShortEng { get; set; }
    }
}
