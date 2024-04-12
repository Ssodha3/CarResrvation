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
    public class AccountController : Controller
    {
        private List<User> _users;

        public AccountController()
        {
            string jsonPath = HostingEnvironment.MapPath("~/App_Data/users.json");
            string jsonData = System.IO.File.ReadAllText(jsonPath);
            _users = JsonConvert.DeserializeObject<List<User>>(jsonData);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            var user = _users.Find(u => u.Username == model.Username && u.Password == model.Password);
            if (user != null)
            {
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Success()
        {
            return RedirectToAction("ListReservations");
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult ListReservations()
        {
            string jsonPath = HostingEnvironment.MapPath("~/App_Data/savereservationsdetails.json");
            string jsonData = System.IO.File.ReadAllText(jsonPath);

            List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonData);

            return View(reservations);
        }

    }
}