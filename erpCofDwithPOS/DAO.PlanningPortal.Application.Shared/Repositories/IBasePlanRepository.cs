using zero.Shared.ViewModels.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Repositories
{ 
    public interface IBasePlanRepository
    {
        void PostData(List<RawDailyPlanDTO> list, int count);
    }
}
