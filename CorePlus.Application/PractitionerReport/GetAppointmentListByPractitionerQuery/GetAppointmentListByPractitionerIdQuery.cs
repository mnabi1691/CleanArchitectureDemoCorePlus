using CorePlus.Report.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePlus.Report.Application.PractitionerReport.GetAppointmentListByPractitionerQuery
{
    public class GetAppointmentListByPractitionerIdQuery: IRequest<List<AppointmentListDto>>
    {
        public int PractitionerId { get; set; }

        public class GetAppointmentListByPractitionerIdQueryHandler : IRequestHandler<GetAppointmentListByPractitionerIdQuery, List<AppointmentListDto>>
        {
            private readonly ICorePlusReportDbContext _context;
            private readonly ILogger<GetAppointmentListByPractitionerIdQueryHandler> _logger;

            public GetAppointmentListByPractitionerIdQueryHandler(
                ICorePlusReportDbContext context
                ,ILogger<GetAppointmentListByPractitionerIdQueryHandler> logger)
            {
                _logger = logger;
                _context = context;
            }

            public async Task<List<AppointmentListDto>> Handle(GetAppointmentListByPractitionerIdQuery request, CancellationToken cancellationToken)
            {
                List<AppointmentListDto> list = null;

                try
                {
                    list = await _context.Appointments
                        .Where(a => a.PractitionerId == request.PractitionerId)
                        .Select(a => new AppointmentListDto
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Cost = a.Cost,
                            Revenue = a.Revenue
                        }).ToListAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                return list;
            }
        }
    }
}
