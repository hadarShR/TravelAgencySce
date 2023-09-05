using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TA_project.Models
{
    public class User
    {
        [Required]
        public string Password { get; set; }
        [Key]
        [Required]
        public string userName { get; set; }
        [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "Name is not valid. Please try again...")]
        //[Required]
        public string firstName { get; set; }
        [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "Name is not valid. Please try again...")]
        //[Required]
        public string lastName { get; set; }
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Card is not valid. Please try again...")]
        //[Required]
        public string card { get; set; }
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "Id is not valid. Please try again...")]
        //[Required]
        public string id { get; set; }
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "Three digits is not valid. Please try again...")]
        //[Required]
        public string threeDigits {get;set;}
        //[Required]
        public string monthValidity { get; set; }
        //[Required]
        public string yearValidity { get; set; }

    }
}