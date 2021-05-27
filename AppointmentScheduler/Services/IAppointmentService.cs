using AppointmentScheduler.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Services
{
    public interface IAppointmentService
    {
        public List<HairdresserVM> GetHairdresserList();
        public List<ClientVM> GetClientList();

    }
}
