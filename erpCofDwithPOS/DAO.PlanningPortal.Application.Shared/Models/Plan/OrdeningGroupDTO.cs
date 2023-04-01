using System.Collections.Generic;
namespace zero.Shared.Models.Plan
{
    public class OrdeningGroupDTO
    {
        public int OrdeningGroupId { get; set; }
        public List<OrdeningCardDTO> OrdeningCards { get; set; }
    }


}
