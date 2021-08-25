using CorePlus.Domain.Entities;
using CorePlus.Report.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorePlus.Report.Persistence
{
    public class CorePlusReportDbContext: DbContext, ICorePlusReportDbContext
    {
        public CorePlusReportDbContext(DbContextOptions<CorePlusReportDbContext> options) : base(options)
        { 
        }

        #region 
        public DbSet<Client> Clients { get; set; }
        public DbSet<Practitioner> Practitioners { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }
        #endregion
    }
}
