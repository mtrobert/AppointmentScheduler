using AppointmentScheduler._Utilities;
using AppointmentScheduler.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public IActionResult Index()
        {
            ViewBag.HairdresserList = _appointmentService.GetHairdresserList();
            ViewBag.ClientList = _appointmentService.GetClientList();
            ViewBag.Duration = Helper.GetTimeDropDown();

            return View();
        }
    }
}
