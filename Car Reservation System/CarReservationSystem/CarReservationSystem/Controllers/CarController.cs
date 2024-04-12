using CarReservationSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace CarReservationSystem.Controllers
{
    public class CarController : Controller
    {
        private const string CarsFilePath = "~/App_Data/cars.json"; 
        private const string ReservationsFilePath = "~/App_Data/savereservationsdetails.json"; 

        public ActionResult ReservationDetails(string carModel)
        {
            string jsonPath = HostingEnvironment.MapPath("~/App_Data/reservations.json");
            string jsonData = System.IO.File.ReadAllText(jsonPath);
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonData);

            var carReservations = reservations.Where(r => r.CarModel == carModel).ToList();

            return View(carReservations);
        }

        public ActionResult Reservation(string carModel)
        {
            List<Car> availableCars = LoadCarsFromJson();

            Car selectedCar = availableCars.FirstOrDefault(c => c.Model == carModel);
            if (selectedCar == null)
            {
                return HttpNotFound(); 
            }

            ViewBag.CarModel = selectedCar.Model;
            ViewBag.ReservationCount = selectedCar.ReservationCount;

            return View();
        }

        [HttpPost]
        public ActionResult Reservation(Reservation reservation)
        {
            List<Car> availableCars = LoadCarsFromJson();

            Car selectedCar = availableCars.FirstOrDefault(c => c.Model == reservation.CarModel);
            if (selectedCar == null)
            {
                return HttpNotFound(); 
            }

            selectedCar.ReservationCount++;

            SaveCarsToJson(availableCars);

            SaveReservationToJson(reservation);

            return RedirectToAction("Success", "Car");
        }

        private List<Car> LoadCarsFromJson()
        {
            string filePath = Server.MapPath(CarsFilePath);
            if (!System.IO.File.Exists(filePath))
            {
                return new List<Car>();
            }

            string json = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Car>>(json);
        }
        private void SaveCarsToJson(List<Car> cars)
        {
            string filePath = Server.MapPath(CarsFilePath);
            string json = JsonConvert.SerializeObject(cars, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }
        private void SaveReservationToJson(Reservation reservation)
        {
            string filePath = Server.MapPath(ReservationsFilePath);
            string json = JsonConvert.SerializeObject(reservation, Formatting.Indented);

            if (System.IO.File.Exists(filePath))
            {
                string existingJson = System.IO.File.ReadAllText(filePath);

                if (!string.IsNullOrEmpty(existingJson))
                {
                    json = existingJson.TrimEnd(']') + "," + Environment.NewLine + json + Environment.NewLine + "]";
                }
                else
                {
                    json = "[" + Environment.NewLine + json + Environment.NewLine + "]";
                }
            }
            System.IO.File.WriteAllText(filePath, json);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}