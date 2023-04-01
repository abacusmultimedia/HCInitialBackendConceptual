using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Models.Plan;
     public class UploadPlanCSV
    {

        public string Mail { get; set; }
        public string Order { get; set; }
        public string Route { get; set; }
        public string Return { get; set; }
        public string ALBTid { get; set; }
        public string Courier { get; set; }
        public string FNetRute { get; set; }
        public string District { get; set; }
        public string CityName { get; set; }
        public string Transpost { get; set; }
        public string RouteType { get; set; }
        public string PriceGroup { get; set; }
        public string RouteLength { get; set; }
        public string PaymentAgainstPersonalVeh { get; set; }
    }
 