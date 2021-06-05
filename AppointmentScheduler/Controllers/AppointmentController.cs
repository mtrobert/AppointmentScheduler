using AppointmentScheduler._Utilities;
using AppointmentScheduler.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        //[Authorize(Roles = Helper.Admin)] <- an example of authorization
        public IActionResult Index()
        {
            ViewBag.HairdresserList = _appointmentService.GetHairdresserList();
            ViewBag.ClientList = _appointmentService.GetClientList();
            ViewBag.Duration = Helper.GetTimeDropDown();

            return View();
        }
    }
}
