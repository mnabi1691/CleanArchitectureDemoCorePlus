using CorePlus.Report.Application.PractitionerReport.GetPractitionerReportList;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorePlus.Report.Application.PractitionerReport.GetPractitionerReportListQuery
{
    public class ReportDto
    {
        public int PractitionerId { get; set; }

        public string PractitionerName { get; set; }

        public List<PractionerReportListDto> ReportList { get; set; }
    }
}
