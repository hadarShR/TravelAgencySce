using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TA_project
{
    public class FlightTicket
    {
        
        [Required]
        [StringLength(6)]
        public string idT { get; set; }
        [Required]
        public string type2 { get; set; }
        [Key]
        [Required]
        public string idF { get; set; }
        public string originCountry { get; set; }
        [Required]
        public string destination { get; set; }
        [Required]
        public DateTime go1 { get; set; }
        [Required]
        public int amountTickets { get; set; }
        [Required]
        public string time1 { get; set; }
        [Required]
        public int price { get; set; }
    }
}