using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TA_project.Models;

namespace TA_project.ViewModel
{
    public class UserViewModel
    {
        public User user { get; set; }
        public List<User> users { get; set; }
    }
}