using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TA_project.Models;

namespace TA_project.Dal
{
    public class TicketDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FlightTicket>().ToTable("ticketTbl1");
        }
        public DbSet<FlightTicket> tickets { get; set; }
    }
}