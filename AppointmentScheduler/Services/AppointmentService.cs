using AppointmentScheduler._Utilities;
using AppointmentScheduler.Models;
using AppointmentScheduler.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;

        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddUpdate(AppointmentVM appointmentVM)
        {
            var startDate = DateTime.Parse(appointmentVM.StartDate);
            var endDate = DateTime.Parse(appointmentVM.StartDate).AddMinutes(Convert.ToDouble(appointmentVM.Duration));

            if (appointmentVM != null && appointmentVM.Id > 0)
            {
                //update appointment
                return 1;
            }
            else
            {
                //create appointment
                Appointment appointment = new Appointment
                {
                    Title = appointmentVM.Title,
                    Desctiption = appointmentVM.Desctiption,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = appointmentVM.Duration,
                    HairdresserId = appointmentVM.HairdresserId,
                    ClientId = appointmentVM.ClientId,
                    IsHairdresserApproved = false,
                    AdminId = appointmentVM.AdminId
                };

                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }
        }

        public List<ClientVM> GetClientList()
        {
            var clients = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(role => role.Name == Helper.Client) on userRoles.RoleId equals roles.Id
                           select new ClientVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }).ToList();

            return clients;
        }

        public List<HairdresserVM> GetHairdresserList()
        {
            var haidressers = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(role => role.Name == Helper.Hairdresser) on userRoles.RoleId equals roles.Id
                           select new HairdresserVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }).ToList();

            return haidressers;
        }
    }
}
