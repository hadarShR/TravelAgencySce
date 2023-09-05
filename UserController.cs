using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TA_project.Dal;
using TA_project.Models;
using TA_project.ViewModel;

namespace TA_project.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        TicketDal ftD = new TicketDal();
        TicketViewModel tvm = new TicketViewModel();
        FlightDal flD = new FlightDal();
        FlightViewModel fvm = new FlightViewModel();
        UserDal fuD = new UserDal();
        UserViewModel uvm = new UserViewModel();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserHomePage()
        {
            List<Flight> flights1 = flD.flights.ToList();
            fvm.flight = new Flight();
            fvm.flightsList = flights1;
            return View(fvm);
        }

        [HttpPost, ActionName("UserHomePage")]
        public ActionResult UserHomePage(bool checkQuery)
        {
            List<Flight> flights1 = flD.flights.ToList();
            FlightViewModel fvm = new FlightViewModel();
            fvm.flightsList = flights1;
            FlightDal dal = new FlightDal();

            fvm.flight = new Flight();

            string myQuery = "";
            string myQuery2 = ""; 
            if (Request.Form["searchQuery"].ToString() == "-- select an option --" && 
                Request.Form["searchQuery2"].ToString() == "-- select an option --")
            {
                myQuery = null;
                myQuery2 = null;
            }
            else if (Request.Form["searchQuery"].ToString() != "-- select an option --" && 
                Request.Form["searchQuery2"].ToString() == "-- select an option --")
            {
                myQuery = Request.Form["searchQuery"].ToString();
                fvm.flightsList = (from x in dal.flights where x.originCountry.Contains(myQuery) select x).ToList<Flight>();
            }
            else if (Request.Form["searchQuery"].ToString() == "-- select an option --" && 
                Request.Form["searchQuery2"].ToString() != "-- select an option --")
            {
                myQuery2 = Request.Form["searchQuery2"].ToString();
                fvm.flightsList = (from x in dal.flights where x.destination.Contains(myQuery2) select x).ToList<Flight>();
            }
            else if (Request.Form["searchQuery"].ToString() != "-- select an option --" && 
                Request.Form["searchQuery2"].ToString() != "-- select an option --")
            {
                if (checkQuery == true)
                {
                    myQuery = Request.Form["searchQuery"].ToString();
                    myQuery2 = Request.Form["searchQuery2"].ToString();
                    fvm.flightsList = (from x in dal.flights where (x.originCountry.Contains(myQuery) &&
                                       x.destination.Contains(myQuery2))
                                       || (x.destination.Contains(myQuery) &&
                                       x.originCountry.Contains(myQuery2)) select x).ToList<Flight>();
                }
                else
                {
                    myQuery = Request.Form["searchQuery"].ToString();
                    myQuery2 = Request.Form["searchQuery2"].ToString();
                    fvm.flightsList = (from x in dal.flights where x.originCountry.Contains(myQuery) &&
                                       x.destination.Contains(myQuery2)
                                       select x).ToList<Flight>();
                }
            }
            DateTime myQuery3 = new DateTime();
            DateTime myQuery4 = new DateTime();
            if (Request.Form["searchQuery3"].ToString() == "" && Request.Form["searchQuery4"].ToString() == "")
            {
                myQuery3 = new DateTime();
                myQuery4 = new DateTime();
            }
            else if (Request.Form["searchQuery3"].ToString() == "" && Request.Form["searchQuery4"].ToString() != "")
            {
                ViewBag.error11 = "Please enter the range dates correctly...";
                return View(fvm);
            }
            else if (Request.Form["searchQuery3"].ToString() != "" && Request.Form["searchQuery4"].ToString() == "")
            {
                myQuery3 = Convert.ToDateTime(Request.Form["searchQuery3"]);
                fvm.flightsList = (from x in dal.flights where x.date1.Equals(myQuery3) select x).ToList<Flight>();
            }
            else
            {
                myQuery3 = Convert.ToDateTime(Request.Form["searchQuery3"]);
                myQuery4 = Convert.ToDateTime(Request.Form["searchQuery4"]);
                if (myQuery3 == myQuery4)
                {
                    fvm.flightsList = (from x in dal.flights where x.price.Equals(myQuery3) select x).ToList<Flight>();
                }
                else if (myQuery3 > myQuery4)
                {
                    ViewBag.error11 = "Please enter the range dates correctly...";
                    return View(fvm);
                }
                else if (myQuery != null)
                {
                    fvm.flightsList = (from x in dal.flights
                                       where x.date1 >= myQuery3 && x.date1 <= myQuery4 && x.originCountry.Contains(myQuery) select x).ToList<Flight>();
                }
                else
                {
                    fvm.flightsList = (from x in dal.flights where x.date1 >= myQuery3 && x.date1 <= myQuery4 && ((x.destination.Contains(myQuery2) 
                                       && x.originCountry.Contains(myQuery)) || (x.originCountry.Contains(myQuery2)
                                       && x.destination.Contains(myQuery))) select x).ToList<Flight>();
                }
            }
            int myQuery51 = 0;
            int myQuery52 = 0;
            if (Request.Form["searchQuery51"].ToString() == "" && Request.Form["searchQuery52"].ToString() == "")
            {
                myQuery51 = 0;
                myQuery52 = 0;
            }
            else if ((Request.Form["searchQuery51"].ToString() != "" && Request.Form["searchQuery52"].ToString() == "") ||
                (Request.Form["searchQuery51"].ToString() == "" && Request.Form["searchQuery52"].ToString() != ""))
            {
                ViewBag.error10 = "Please enter the range prices correctly...";
                return View(fvm);
            }
            else
            {
                myQuery51 = int.Parse(Request.Form["searchQuery51"]);
                myQuery52 = int.Parse(Request.Form["searchQuery52"]);
                if (myQuery51 == myQuery52)
                {
                    fvm.flightsList = (from x in dal.flights where x.price.Equals(myQuery51) select x).ToList<Flight>();
                }
                else if (myQuery51 > myQuery52)
                {
                    ViewBag.error10 = "Please enter the range prices correctly...";
                    return View(fvm);
                }
                else
                {
                    fvm.flightsList = (from x in dal.flights where (x.price >= myQuery51 && x.price <= myQuery52) select x).ToList<Flight>();
                }
            }
            string myQuery6 = "";
            if (Request.Form["searchQuery6"].ToString() == "")
            {
                myQuery6 = null;
            }
            else
            {
                myQuery6 = Request.Form["searchQuery6"].ToString();
                fvm.flightsList = (from x in dal.flights where x.time.Contains(myQuery6) select x).ToList<Flight>();
            }
            if (myQuery == null && myQuery2 == null && myQuery3 == null && myQuery4 == null && myQuery51 == 0 && myQuery6 == null)
            {
                fvm.flightsList = dal.flights.ToList<Flight>();
            }
            if (fvm.flightsList.Count == 0)
            {
                ViewBag.notfound = "Sorry, there are no suitable flights for your search...";
                return View(fvm);
            }
            return View("UserHomePage", fvm);
        }

        private static Random random = new Random();

        private static string RandomString(int length)
        {
            const string pool = "ABCDEFGHIJKLMNPOQRSTUVWXVZ0123456789";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var c = pool[random.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }

        public ActionResult Buy(string id)
        {
            FlightViewModel fvm = new FlightViewModel();
            fvm.flightsList = flD.flights.ToList<Flight>();
            List<FlightTicket> objFT = ftD.tickets.ToList();
            List<Flight> objFlights = flD.flights.ToList();
            var ft1 = flD.flights.Where(x => x.id == id).FirstOrDefault();
            FlightTicket ft = new FlightTicket();
            FlightTicket fli = ftD.tickets.Find(id);

            ft.idT = RandomString(6);
            if (ft1.tickets == 0)
            {
                ViewBag.error66 = "Flight is sold out. Please try again...";
                return View("UserHomePage", fvm);
            }
            else if (fli != null)
            {                
                fli.amountTickets += 1;
                ftD.SaveChanges();
                return RedirectToAction("ShoppingCart");
            }
            else
            {
                ft.idF = ft1.id;
                ft.type2 = ft1.type1;
                ft.go1 = ft1.date1;
                ft.originCountry = ft1.originCountry;
                ft.destination = ft1.destination;
                ft.time1 = ft1.time;
                ft.price = ft1.price;
                ft.amountTickets = 1;
                ftD.tickets.Add(ft);
                ftD.SaveChanges();
                return RedirectToAction("ShoppingCart");
            }            
        }

        public ActionResult Save()
        {
            int newAmount = 0;
            List<FlightTicket> objTickets = ftD.tickets.ToList();
            TicketViewModel tvm = new TicketViewModel();
            tvm.ticketsList = objTickets;
            foreach (FlightTicket x in objTickets)
            {
                if (x.idF == Request.Form["idF"].ToString())
                {
                    Flight fli = flD.flights.Find(x.idF);
                    newAmount = int.Parse(Request.Form["numTicket"]);
                    if (fli.tickets < int.Parse(Request.Form["numTicket"]))
                    {
                        ViewBag.error70 = "There are not enough tickets available. Please try again...";
                        return View("ShoppingCart", tvm);
                    }
                    else if (fli.type1 == "small" && x.amountTickets > 150)
                    {
                        ViewBag.error70 = "There are not enough tickets available. Please try again...";
                        return View("ShoppingCart",tvm);
                    }
                    else if (fli.type1 == "medium" && x.amountTickets > 250)
                    {
                        ViewBag.error70 = "There are not enough tickets available. Please try again...";
                        return View("ShoppingCart", tvm);
                    }
                    else if (fli.type1 == "medium" && x.amountTickets > 400)
                    {
                        ViewBag.error70 = "There are not enough tickets available. Please try again...";
                        return View("ShoppingCart", tvm);
                    }                    
                    else
                    {
                        x.amountTickets = newAmount;
                    }
                }
                else
                {
                    ViewBag.error71 = "Flight is not exists. Please try again...";
                    return View("ShoppingCart", tvm);
                }
            }

            ftD.SaveChanges();
            return RedirectToAction("ShoppingCart"); 
        }

        public ActionResult ShoppingCart()
        {
            List<FlightTicket> ft = ftD.tickets.ToList();
            tvm.flightTicket = new FlightTicket();
            tvm.ticketsList = ft;
            return View(tvm);
        }

        public ActionResult DeleteTicket(string id)
        {
            var data = ftD.tickets.Where(x => x.idT == id).FirstOrDefault();
            ftD.tickets.Remove(data);
            ftD.SaveChanges();
            ViewBag.Messsage = "Record Delete Successfully";
            return RedirectToAction("ShoppingCart");
        }

        public ActionResult Payment()
        {
            FlightViewModel fvm = new FlightViewModel();
            fvm.flightsList = flD.flights.ToList<Flight>();
            TicketViewModel tvm = new TicketViewModel();
            List<FlightTicket> objTickets = ftD.tickets.ToList();
            tvm.ticketsList = objTickets;
            if (tvm.ticketsList.Count == 0)
            {
                ViewBag.emptylist = "Your shopping cart is empty";
                return View("UserHomePage", fvm);
            }
            List<User> objUser = fuD.Users.ToList();
            uvm.user = new User();
            uvm.users = objUser;
            int sum = 0;

            foreach (FlightTicket x in objTickets)
            {
                sum += x.amountTickets * x.price;
            }
            ViewBag.price = sum;
            uvm.user.userName = Request.Form["User.userName"];
            return View(uvm);
        }

        [HttpPost, ActionName("Payment")]
        public ActionResult Payment(bool saveCard, User user, bool flag)
        {
            int sum = 0;
            List<FlightTicket> objTickets = ftD.tickets.ToList();
            TicketViewModel tvm = new TicketViewModel();
            tvm.ticketsList = objTickets;
            int month = DateTime.Now.Month;           
            int year = DateTime.Now.Year;

            foreach (FlightTicket x in objTickets)
            {
                sum += x.amountTickets * x.price;
            }
            ViewBag.price = sum;
            List<User> objUser2 = fuD.Users.ToList();
            uvm.user = new User();            
            uvm.users = objUser2;
            User objUser = user;
            objUser.userName = Session["currUserName"].ToString();
            objUser.Password = Session["currPassword"].ToString();
            string myId = Request.Form["id"].ToString();
            List<User> a = (from x in fuD.Users where x.id.Contains(myId) select x).ToList<User>();
            uvm.user = new User();
            uvm.user.userName = Request.Form["User.userName"];
            if (flag == true)
            {
                return RedirectToAction("Pay");
            }
            else
            {
                if (saveCard == true)
                {
                    if (Request.Form["firstName"].ToString() == "")
                    {
                        ViewBag.errorname = "Please enter a name";
                        return View(uvm);
                    }
                    else
                    {
                        objUser.firstName = Request.Form["firstName"].ToString();
                    }
                    if (Request.Form["lastName"].ToString() == "")
                    {
                        ViewBag.errorname = "Please enter a name";
                    }
                    else
                    {
                        objUser.lastName = Request.Form["lastName"].ToString();
                    }
                    if (Request.Form["id"].ToString() == "")
                    {
                        ViewBag.errorid = "Please enter ID";
                        return View(uvm);
                    }
                    else if (a.Count != 0)
                    {
                        ViewBag.error = "Id already exsits. Please try again...";
                        return View(uvm);
                    }
                    else
                    {
                        objUser.id = Request.Form["id"].ToString();
                    }
                    if (Request.Form["card"].ToString() == "")
                    {
                        ViewBag.errorcard = "Please enter card numbers";
                        return View(uvm);
                    }
                    else
                    {
                        objUser.card = Request.Form["card"].ToString();
                    }
                    if (Request.Form["threeDigits"].ToString() == "")
                    {
                        ViewBag.error3 = "Please enter three digits on the back of the card";
                        return View(uvm);
                    }
                    else
                    {
                        objUser.threeDigits = Request.Form["threeDigits"].ToString();
                    }
                    if (Request.Form["monthValidity"].ToString() == "")
                    {
                        ViewBag.errormonth = "Please select month";
                        return View(uvm);
                    }
                    else
                    {
                        objUser.monthValidity = Request.Form["monthValidity"].ToString();
                    }
                    if (Request.Form["yearValidity"].ToString() == "")
                    {
                        ViewBag.erroryear = "Please select year";
                        return View(uvm);
                    }
                    else
                    {
                        objUser.yearValidity = Request.Form["yearValidity"].ToString();
                    }
                    if (int.Parse(Request.Form["yearValidity"]) == year)
                    {
                        if (int.Parse(Request.Form["monthValidity"]) <= month)
                        {
                            ViewBag.errordate = "Invalid validity";
                            return View(uvm);
                        }
                    }
                    foreach (User x in uvm.users)
                    {
                        User u = fuD.Users.Find(x.userName);
                        if (x.userName == objUser.userName)
                        {
                            fuD.Users.Remove(x);
                            fuD.Users.Add(objUser);
                            fuD.SaveChanges();
                            break;
                        }
                    }
                    return RedirectToAction("Pay");
                }
                else
                {
                    if (Request.Form["firstName"].ToString() == "")
                    {
                        ViewBag.errorname = "Please enter a name";
                        return View(uvm);
                    }
                    if (Request.Form["lastName"].ToString() == "")
                    {
                        ViewBag.errorname = "Please enter a name";
                        return View(uvm);
                    }
                    if (Request.Form["id"].ToString() == "")
                    {
                        ViewBag.errorid = "Please enter ID";
                        return View(uvm);
                    }
                    else if (a.Count != 0)
                    {
                        ViewBag.error = "Id already exsits. Please try again...";
                        return View(uvm);
                    }
                    if (Request.Form["card"].ToString() == "")
                    {
                        ViewBag.errorcard = "Please enter card numbers";
                        return View(uvm);
                    }
                    if (Request.Form["threeDigits"].ToString() == "")
                    {
                        ViewBag.error3 = "Please enter three digits on the back of the card";
                        return View(uvm);
                    }
                    if (Request.Form["monthValidity"].ToString() == "")
                    {
                        ViewBag.errormonth = "Please select month";
                        return View(uvm);
                    }
                    if (Request.Form["yearValidity"].ToString() == "")
                    {
                        ViewBag.erroryear = "Please select year";
                        return View(uvm);
                    }
                    if (int.Parse(Request.Form["yearValidity"]) == year)
                    {
                        if (int.Parse(Request.Form["monthValidity"]) <= month)
                        {
                            ViewBag.errordate = "Invalid validity";
                            return View(uvm);
                        }
                    }
                    return RedirectToAction("Pay");
                }
            }
        }

        public ActionResult Pay()
        {
            foreach (Flight fli in flD.flights)
            {
                foreach (FlightTicket ft in ftD.tickets)
                {
                    if (fli.id == ft.idF)
                    {
                        fli.tickets -= ft.amountTickets;
                        ftD.tickets.Remove(ft);                        
                    }                   
                }
                ftD.SaveChanges();
            }
            flD.SaveChanges();            
            return View();
        }
    }
}