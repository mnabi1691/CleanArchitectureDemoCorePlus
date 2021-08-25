using System;
using System.Collections.Generic;
using System.Text;

namespace CorePlus.Report.Application.PractitionerReport.GetPractitionerReportList
{
    public class PractionerReportListDto
    {
        public decimal? RevenuePerMonth { get; set; }

        public decimal? CostPerMonth { get; set; }

        public string Month { get; set; }
    }
}
