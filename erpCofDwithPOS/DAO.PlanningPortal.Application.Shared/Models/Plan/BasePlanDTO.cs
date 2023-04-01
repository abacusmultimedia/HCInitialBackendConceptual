using System.Collections.Generic;
namespace zero.Shared.Models.Plan
{
    public class BasePlanDTO
    {
        public string OrganizationalUnitName { get; set; }
        public int OrganizationUnitId { get; set; }
        public List<OrdeningGroupDTO> OrdeningGroup { get; set; }
        public CRouteGroupDTO CRouteGroup { get; set; }
    }

 

    public class KeyValueWithChildern
    {
        public string Key { get; set; }
        public int ChildCount { get; set; }

        public List<KeyValueWithChildern> Children { get; set; } = new List<KeyValueWithChildern>();
    }


}
