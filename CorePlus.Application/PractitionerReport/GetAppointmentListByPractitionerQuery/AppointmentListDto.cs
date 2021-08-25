using System;
using System.Collections.Generic;
using System.Text;

namespace CorePlus.Report.Application.PractitionerReport.GetAppointmentListByPractitionerQuery
{
    public class AppointmentListDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal? Cost { get; set; }

        public decimal? Revenue { get; set; }
    }
}
