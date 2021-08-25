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

namespace CorePlus.Report.Application.PractitionerReport.GetAppointmentDetailsQuery
{
    public class GetAppointmentDetailsQuery: IRequest<AppointmentDetailsDto>
    {
        public int Id { get; set; }

        public class GetAppointmentDetailsQueryHandler : IRequestHandler<GetAppointmentDetailsQuery, AppointmentDetailsDto>
        {
            private readonly ICorePlusReportDbContext _context;
            private readonly ILogger<GetAppointmentDetailsQueryHandler> _logger;

            public GetAppointmentDetailsQueryHandler(ICorePlusReportDbContext context, ILogger<GetAppointmentDetailsQueryHandler> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<AppointmentDetailsDto> Handle(GetAppointmentDetailsQuery request, CancellationToken cancellationToken)
            {
                AppointmentDetailsDto dto = null;

                try
                {
                    dto = await _context.Appointments
                        .Include(a => a.Client)
                        .Include(a => a.Type)
                        .Include(a => a.Practitioner)
                        .Where(a => a.Id == request.Id)
                        .Select(a => new AppointmentDetailsDto
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Date = a.Date,
                            Duration = a.Duration,
                            Revenue = a.Revenue,
                            Cost = a.Cost,
                            ClientName = a.Client.FirstName + " " + a.Client.LastName,
                            PractitionerName = a.Practitioner.FirstName + " " + a.Practitioner.LastName,
                            Type = a.Type.Name
                        }).SingleAsync(cancellationToken);
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
