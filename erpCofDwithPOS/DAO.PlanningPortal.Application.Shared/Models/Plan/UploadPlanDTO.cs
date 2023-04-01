using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.ViewModels.Plan
{
    public class UploadPlanDto
    { 
        public string Name { get; set; }    
        public string Description { get; set; }
        public IFormFile File { get; set; }

    }
}
