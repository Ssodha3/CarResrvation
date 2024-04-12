using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarReservationSystem.Models
{
    public class CarReservation
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string CustomerName { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}