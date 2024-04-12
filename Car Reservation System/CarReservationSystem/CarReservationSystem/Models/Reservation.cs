using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarReservationSystem.Models
{
    public class Reservation
    {
        public string CarModel { get; set; }
        public DateTime DateTime { get; set; }
        public string Username { get; set; }
        public DateTime ReservationDateTime { get; set; }
    }

    public class CarReservationInfo
    {
        public string CarModel { get; set; }
        public int ReservationCount { get; set; }

        public string ImageUrl { get; set; }
        public string ModelName { get; set; }
    }
}