using CorePlus.Report.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using CorePlus.Report.Application.PractitionerReport.GetPractitionerReportListQuery;

namespace CorePlus.Report.Application.PractitionerReport.GetPractitionerReportList
{
    public class GetPractitionerReportListQuery : IRequest<ReportDto>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PractitionerId { get; set; }

        public class GetPractitionerReportListQueryHandler : IRequestHandler<GetPractitionerReportListQuery, ReportDto>
        {
            private readonly ICorePlusReportDbContext _context;
            private readonly ILogger<GetPractitionerReportListQueryHandler> _logger;

            public GetPractitionerReportListQueryHandler(
                ICorePlusReportDbContext context
                ,ILogger<GetPractitionerReportListQueryHandler> logger)
            {
                _logger = logger;
                _context = context;
            }

            public async Task<ReportDto> Handle(GetPractitionerReportListQuery request, CancellationToken cancellationToken)
            {
                ReportDto dto = null;

                try
                {
                    dto = await _context.Practitioners
                        .Where(p => p.Id == request.PractitionerId)
                        .Select(p => new ReportDto
                        {
                            PractitionerId = p.Id,
                            PractitionerName = p.FirstName + " " + p.LastName,
                            ReportList = new List<PractionerReportListDto>()
                        })
                        .SingleAsync();

                    dto.ReportList = (from appointmet in _context.Appointments.AsNoTracking()
                                      where appointmet.Date >= request.StartDate
                                      && appointmet.Date < request.EndDate
                                      && appointmet.PractitionerId == request.PractitionerId
                                      group appointmet by new
                                      {
                                          Year = appointmet.Date.Value.Year,
                                          Month = appointmet.Date.Value.Month,
                                      } into g
                                      select new
                                      {
                                          Year = g.Key.Year,
                                          Month = g.Key.Month,
                                          CostPerMonth = g.Sum(x => x.Cost),
                                          RevenuePerMonth = g.Sum(x => x.Revenue)
                                      })
                                            .AsEnumerable()
                                            .Select(g => new PractionerReportListDto
                                            {
                                                Month = g.Year.ToString() + "_" + g.Month.ToString(),
                                                CostPerMonth = g.CostPerMonth,
                                                RevenuePerMonth = g.RevenuePerMonth
                                            })
                                            .ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                return dto;
            }
        }
    }
}
