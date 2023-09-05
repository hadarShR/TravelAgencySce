using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TA_project.Models;

namespace TA_project.ViewModel
{
    public class TicketViewModel
    {
        public FlightTicket flightTicket { get; set; }
        public List<FlightTicket> ticketsList { get; set; }
    }
}