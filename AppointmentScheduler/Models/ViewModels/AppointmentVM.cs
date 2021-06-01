using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Models.ViewModels
{
    public class AppointmentVM
    {       
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Desctiption { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Duration { get; set; }
        public string HairdresserId { get; set; }
        public string ClientId { get; set; }
        public string AdminId { get; set; }
        public bool IsDoctorApproved { get; set; }

        public string HairdresserName { get; set; }
        public string ClientName{ get; set; }
        public string AdminName { get; set; }
        public bool IsForClient { get; set; }
    }
}
