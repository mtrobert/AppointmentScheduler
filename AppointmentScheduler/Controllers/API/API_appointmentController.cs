using AppointmentScheduler._Utilities;
using AppointmentScheduler.Models.ViewModels;
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

        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentVM appointmentVM)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();

            try
            {
                commonResponse.Status = _aS.AddUpdate(appointmentVM).Result;
                if (commonResponse.Status == 1)
                {
                    commonResponse.Message = Helper.appointmentUpdated;
                }

                if (commonResponse.Status == 2)
                {
                    commonResponse.Message = Helper.appointmentAdded;
                }
            }
            catch (Exception e)
            {

                commonResponse.Message = e.Message;
                commonResponse.Status = Helper.FailureCode;
            }

            return Ok(commonResponse);
        }
    }
}
