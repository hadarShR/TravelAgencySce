using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TA_project.Models;

namespace TA_project.ViewModel
{
    public class FlightViewModel
    {
        public Flight flight { get; set; }
        public List<Flight> flightsList { get; set; }
    }
}