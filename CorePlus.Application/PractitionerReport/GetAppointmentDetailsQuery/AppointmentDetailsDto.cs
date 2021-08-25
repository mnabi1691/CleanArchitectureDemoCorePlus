using System;
using System.Collections.Generic;
using System.Text;

namespace CorePlus.Report.Application.PractitionerReport.GetAppointmentDetailsQuery
{
    public class AppointmentDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? Date { get; set; }

        public double? Duration { get; set; }

        public decimal? Revenue { get; set; }

        public decimal? Cost { get; set; }

        public string ClientName { get; set; }

        public string Type { get; set; }

        public string PractitionerName { get; set; }
    }
}
