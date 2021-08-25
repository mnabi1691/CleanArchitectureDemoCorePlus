using CorePlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorePlus.Report.Application.Interfaces
{
    public interface ICorePlusReportDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Practitioner> Practitioners { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }
    }
}
