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
