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
using static CorePlus.Report.Application.PractitionerReport.GetPractitionerReportList.GetPractitionerReportListQuery;

namespace CorePlus.Report.Application.PractitionerReport.GetPractitionerSelectItemsQuery
{
    public class GetPractitionerSelectItemsQuery: IRequest<List<PractionerSelectItemDto>>
    {
        public class GetPractitionerSelectItemsQueryHandler : IRequestHandler<GetPractitionerSelectItemsQuery, List<PractionerSelectItemDto>>
        {
            private readonly ICorePlusReportDbContext _context;
            private readonly ILogger<GetPractitionerSelectItemsQueryHandler> _logger;

            public GetPractitionerSelectItemsQueryHandler(ICorePlusReportDbContext context, ILogger<GetPractitionerSelectItemsQueryHandler> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<List<PractionerSelectItemDto>> Handle(GetPractitionerSelectItemsQuery request, CancellationToken cancellationToken)
            {
                List<PractionerSelectItemDto> list = null;

                try
                {
                    list = await _context.Practitioners
                        .Where(p => p.IsActive)
                        .Select(p => new PractionerSelectItemDto
                        {
                            Id = p.Id,
                            Name = p.FirstName + " " + p.LastName
                        })
                        .ToListAsync(cancellationToken);
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
