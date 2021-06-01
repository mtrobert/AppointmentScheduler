using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desctiption { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public string HairdresserId { get; set; }
        public string ClientId { get; set; }
        public string AdminId { get; set; }
        public bool IsHairdresserApproved { get; set; }
    }
}
