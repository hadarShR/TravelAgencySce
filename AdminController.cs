using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using TA_project.Dal;
using TA_project.Models;
using TA_project.ViewModel;

namespace TA_project.Controllers
{
    public class AdminController : Controller
    {
        FlightDal flD = new FlightDal();
        FlightViewModel fvm = new FlightViewModel();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddFlight()
        {
          
            return View();
        }

        [HttpPost, ActionName("AddFlight")]
        public ActionResult AddFlight(Flight fli)
        {
            string tempId = fli.id;
            string tempOrigin = fli.originCountry;
            string tempDestination = fli.destination;
            string tempType = fli.type1;
            int tempTickets = fli.tickets;
            List<Flight> objFlights = flD.flights.ToList();
            fvm.flight = fli;
            foreach (Flight x in objFlights)
            {
                if (x.id == tempId)
                {
                    ViewBag.error = "Id already exsits. Please try again...";
                    return View(fvm);
                }
                if (tempOrigin == fli.destination || tempDestination == fli.originCountry)
                {
                    ViewBag.error5 = "Origin country and destination can not be the same. Please try again...";
                    return View(fvm);
                }
                if (tempType == "little")
                {
                    if (tempTickets < 100 || tempTickets > 150)
                    {
                        ViewBag.error20 = "If type is little so tickets can be only 100-150";
                        return View(fvm);                        
                    }
                    
                }
                if (tempType == "medium")
                {
                    if (tempTickets < 150 || tempTickets > 250)
                    {
                        ViewBag.error21 = "If type is medium so tickets can be only 150-250";
                        return View(fvm);
                    }

                }
                if (tempType == "big")
                {
                    if (tempTickets < 250 || tempTickets > 400)
                    {
                        ViewBag.error21 = "If type is big so tickets can be only 250-400";
                        return View(fvm);
                    }

                }

            }
            if (ModelState.IsValid)
            {
                flD.flights.Add(fli);
                flD.SaveChanges();
                return RedirectToAction("FlightsPage");
            }
            return View();
        }

        public ActionResult FlightsPage()
        {
            List<Flight> flights1 = flD.flights.ToList();
            fvm.flight = new Flight();
            fvm.flightsList = flights1;
            return View(fvm);
        }

        /*public ActionResult Delete(string id)
        {
            Flight fl = flD.flights.Find(id);
            if (fl == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Flight fl)
        {
            Flight fli = flD.flights.Find(fl.id);

            flD.flights.Remove(fli);
            flD.SaveChanges();
            return View("FlightsPage",fvm);
        }*/

        public ActionResult Delete(string id)
        {
            var data = flD.flights.Where(x => x.id == id).FirstOrDefault();
            flD.flights.Remove(data);
            flD.SaveChanges();
            ViewBag.Messsage = "Record Delete Successfully";
            return RedirectToAction("FlightsPage");
        }
        public ActionResult Edit(string id)
        {
            Flight fl = flD.flights.Find(id);
            if (fl == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(Flight fli)
        {
            string tempOrigin = fli.originCountry;
            string tempDestination = fli.destination;
            string tempType = fli.type1;
            int tempTickets = fli.tickets;
            List<Flight> objFlights = flD.flights.ToList();
            fvm.flight = fli;
            foreach (Flight x in objFlights)
            {
                if (tempOrigin == fli.destination || tempDestination == fli.originCountry)
                {
                    ViewBag.error5 = "Origin country and destination can not be the same. Please try again...";
                    return View(fvm);
                }
                if (tempType == "little")
                {
                    if (tempTickets < 100 || tempTickets > 150)
                    {
                        ViewBag.error20 = "If type is little so tickets can be only 100-150";
                        return View(fvm);
                    }

                }
                if (tempType == "medium")
                {
                    if (tempTickets < 150 || tempTickets > 250)
                    {
                        ViewBag.error21 = "If type is medium so tickets can be only 150-250";
                        return View(fvm);
                    }

                }
                if (tempType == "big")
                {
                    if (tempTickets < 250 || tempTickets > 400)
                    {
                        ViewBag.error21 = "If type is big so tickets can be only 250-400";
                        return View(fvm);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                //flD.Entry(fli).State = EntityState.Modified;
                var et = flD.flights.Find(fli.id);
                flD.Entry(et).CurrentValues.SetValues(fli);
                flD.SaveChanges();
                return RedirectToAction("FlightsPage");
            }
            return View(fli);
        }


    }



}