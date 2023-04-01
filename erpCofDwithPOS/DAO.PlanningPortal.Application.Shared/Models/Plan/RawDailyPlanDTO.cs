using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.ViewModels.Plan
{
    public class RawDailyPlanDTO
    {
        public int Mail { get; set; }
        public int Day { get; set; }
        public int Courier { get; set; }
        public bool Return { get; set; }
        public string Route { get; set; }
        public int District { get; set; }
        public string Order { get; set; }
        public int FNetRute { get; set; }
        public double ALBTid { get; set; }
        public string CityName { get; set; }
        public string Transpost { get; set; }
        public string RouteType { get; set; }
        public string PriceGroup { get; set; }
        public double RouteLength { get; set; }
        public bool PaymentAgainstPersonalVeh { get; set; }
    }
}
