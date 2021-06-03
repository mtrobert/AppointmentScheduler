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
        public Task<int> AddUpdate(AppointmentVM appointmentVM);
        public List<AppointmentVM> HairdresserEventsById(string hairdresserId);
        public List<AppointmentVM> ClientEventsById(string clientId);
        public AppointmentVM GetById(int id);
        public Task<int> Delete(int id);
        public Task<int> ConfirmEvent(int id);
    }
}
