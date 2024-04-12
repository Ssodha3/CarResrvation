using CarReservationSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace CarReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {  
           //get json data and store in list
            string jsonPath = HostingEnvironment.MapPath("~/App_Data/savereservationsdetails.json");
            string jsonData = System.IO.File.ReadAllText(jsonPath);
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonData);

            //if there are reservation display and if no reservation then msg
            if (reservations != null)
            {
                var carReservationCounts = reservations
                    .GroupBy(r => r.CarModel)
                    .Select(g => new CarReservationInfo { CarModel = g.Key, ReservationCount = g.Count() })
                    .ToList();
                
                if (carReservationCounts == null || carReservationCounts.Count == 0)
                {
                    ViewBag.CarReservationCounts = null;
                    ViewBag.NoReservationsMessage = "No reservations available.";
                }
                else
                {
                    ViewBag.CarReservationCounts = carReservationCounts;
                    ViewBag.NoReservationsMessage = null;
                }
            }
            else
            {
                ViewBag.CarReservationCounts = null;
                ViewBag.NoReservationsMessage = "No reservations available.";
            }


            var staticCars = new List<CarReservationInfo>
            {
               new CarReservationInfo { CarModel = "Toyota Camry",  ImageUrl = "/images/1.jpg", ModelName = "Camry" },
               new CarReservationInfo { CarModel = "Honda Civic",  ImageUrl = "/images/2.jpg", ModelName = "Civic EX" },
               new CarReservationInfo { CarModel = "Ford Mustang",  ImageUrl = "/images/3.jpg", ModelName = "Mustang" },
               new CarReservationInfo { CarModel = "BMW 3 Series", ImageUrl = "/images/4.jpg", ModelName = "3 Series"},
               new CarReservationInfo { CarModel = "Tesla Model S",  ImageUrl = "/images/5.jpg", ModelName = "Model S" },
               new CarReservationInfo { CarModel = "Chevrolet Corvette",  ImageUrl = "/images/6.jpg", ModelName = "Corvette" },
            
            };
            ViewBag.Data = staticCars;
            return View();
        }


    }
}