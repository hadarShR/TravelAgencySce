﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TA_project.Models;

namespace TA_project.Dal
{
    public class FlightDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flight>().ToTable("flightsTbl");
        }
        public DbSet<Flight> flights
        {
            get; set;
        }
    }
}