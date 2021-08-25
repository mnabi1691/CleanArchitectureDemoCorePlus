using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorePlus.Report.Application.PractitionerReport.GetAppointmentDetailsQuery;
using CorePlus.Report.Application.PractitionerReport.GetAppointmentListByPractitionerQuery;
using CorePlus.Report.Application.PractitionerReport.GetPractitionerReportList;
using CorePlus.Report.Application.PractitionerReport.GetPractitionerReportListQuery;
using CorePlus.Report.Application.PractitionerReport.GetPractitionerSelectItemsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorePlus.Report.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PractitionerReportController : ControllerBase
    {
        private IMediator _mediatr;
        public PractitionerReportController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetDummy()
        {
            return Ok("Api is running");
        }

        [Route("{id:int}/{startDate}/{endDate}")]
        [HttpGet]
        [Authorize(Roles = "AccountSupervisor")]
        public async Task<IActionResult> GetPractitionerReportList(string startDate, string endDate, int id)
        {
            ReportDto report;
            try
            {
                //Parse datetime from query string. 
                DateTime s, e;
                DateTime.TryParse(startDate, out s);
                DateTime.TryParse(endDate, out e);

                report =
                    await _mediatr.Send(new GetPractitionerReportListQuery { StartDate = s, EndDate = e, PractitionerId = id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal Server Error: " + ex.Message);
            }

            return Ok(report);
        }

        [Route("appointmentlist/bypractionerid/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "AccountSupervisor")]
        public async Task<IActionResult> GetAppointmentListForPractioner(int id)
        {
            List<AppointmentListDto> list;
            try
            {
                list =
                    await _mediatr.Send(new GetAppointmentListByPractitionerIdQuery { PractitionerId = id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal Server Error: " + ex.Message);
            }

            return Ok(list);
        }

        [Route("appointment/id/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "AccountSupervisor")]
        public async Task<IActionResult> GetAppointmentDetailsById(int id)
        {
            AppointmentDetailsDto appointment;
            try
            {
                appointment =
                    await _mediatr.Send(new GetAppointmentDetailsQuery { Id = id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal Server Error: " + ex.Message);
            }

            return Ok(appointment);
        }

        [Route("practitionerselectitems")]
        [HttpGet]
        [Authorize(Roles = "AccountSupervisor")]
        public async Task<IActionResult> GetPractitionerSelectItems()
        {
            List<PractionerSelectItemDto> list = null;
            try
            {
                list =
                    await _mediatr.Send(new GetPractitionerSelectItemsQuery());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal Server Error: " + ex.Message);
            }

            return Ok(list);
        }
    }
}
