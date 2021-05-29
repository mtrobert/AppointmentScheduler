using AppointmentScheduler.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppointmentScheduler.Controllers.API
{
    [Route("api/Appointment")]
    [ApiController]
    public class API_appointmentController : Controller
    {
        private readonly IAppointmentService _aS;
        private readonly IHttpContextAccessor _httpCA;
        private readonly string _loginUserId;
        private readonly string _role;

        public API_appointmentController(IAppointmentService aS, IHttpContextAccessor httpCA)
        {
            _aS = aS;
            _httpCA = httpCA;
            _loginUserId = _httpCA.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _role = _httpCA.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
