using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.OData.Edm;

namespace TA_project.Models
{
    public class Flight
    {
        [Key]
        [Required]
        [StringLength(6, ErrorMessage = "Id must be 6 letters. Please try again...")]
        public string id { get; set; }
        public string originCountry { get; set; }
        [Required]
        public string destination { get; set; }
        [Required]
        //[Display(Name = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime date1 { get; set; }
        [Required]
        //[RegularExpression("^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Time is not valid. Please try again...")]
        public string time { get; set; }
        [Required]
        [RegularExpression("^[0-9]{1,4}$", ErrorMessage = "Price must contain only numbers 0-9999. Please try again...")]
        public int price { get; set; }
        [Required]
        [RegularExpression("^[0-9]{1,3}$", ErrorMessage = "Tickets must contain only numbers 0-999. Please try again...")]
        public int tickets { get; set; }
        [Required]
        public string type1 { get; set; }
    }
}