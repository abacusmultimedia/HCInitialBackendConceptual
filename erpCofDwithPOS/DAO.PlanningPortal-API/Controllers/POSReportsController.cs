using DAO.PlanningPortal.Application.Interfaces.POS;
using ERP.API.Common.Controller;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using zero.Shared.Models.POS;
using zero.Shared.Response;

namespace DAO.PlanningPortal.API.Controllers
{
    [Route("/api/POSReports")]
    public class POSReportsController : BaseApiController
    {
        public POSReportsController(IPOS appService)
        {
            _appService = appService;
        }

        public IPOS _appService { get; }

        [HttpPost]
        [Route("GetUserReport")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<List<POSUserReport>>))]
        public async Task<ActionResult<List<POSUserReport>>> GetUserReport(ReportFilters filters)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetUserReport(filters);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpPost]
        [Route("GetCategoryReport")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<POSCategoryReportWithTotal>))]
        public async Task<ActionResult<POSCategoryReportWithTotal>> GetCategoryReport(ReportFilters filters)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetCategoryWiseReport(filters);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }
        [HttpPost]
        [Route("GetTotalSalesReport")]
        [ProducesDefaultResponseType(typeof(ServiceResponse<TotalSalesReportDTO>))]
        public async Task<ActionResult<TotalSalesReportDTO>> GetTotalSalesReport(ReportFilters filters)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(GetModelErrorResponse(ModelState));
            }
            var response = await _appService.GetTotalSalesReport(filters);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }
    }
}
