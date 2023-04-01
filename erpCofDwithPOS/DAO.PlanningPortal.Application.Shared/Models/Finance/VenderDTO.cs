using zero.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.Finance
{
    public class VenderDTO  : PersonalInfoDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
