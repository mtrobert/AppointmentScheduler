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

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string hairdresserId)
        {
            CommonResponse<List<AppointmentVM>> commonResponse = new CommonResponse<List<AppointmentVM>>();
            try
            {
                if (_role == Helper.Client)
                {
                    commonResponse.Data_Enum = _aS.ClientEventsById(_loginUserId);
                    commonResponse.Status = Helper.SuccessCode;
                }
                else if(_role == Helper.Hairdresser)
                {
                    commonResponse.Data_Enum = _aS.HairdresserEventsById(_loginUserId);
                    commonResponse.Status = Helper.SuccessCode;
                }
                else
                {
                    commonResponse.Data_Enum = _aS.HairdresserEventsById(hairdresserId);
                    commonResponse.Status = Helper.SuccessCode;
                }
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = Helper.FailureCode;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarDataById/{appointmentId}")]
        public IActionResult GetCalendarDataById(int appointmentId)
        {
            CommonResponse<AppointmentVM> commonResponse = new CommonResponse<AppointmentVM>();
            try
            {              
                commonResponse.Data_Enum = _aS.GetById(appointmentId);
                commonResponse.Status = Helper.SuccessCode;
                
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = Helper.FailureCode;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("ConfirmAppointment/{appointmentId}")]
        public IActionResult ConfirmAppointment(int appointmentId)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                var result = _aS.ConfirmEvent(appointmentId).Result;

                if (result > 0)
                {
                    commonResponse.Status = Helper.SuccessCode;
                    commonResponse.Message = Helper.appointmentConfirmed;
                }
                else
                {
                    commonResponse.Status = Helper.FailureCode;
                    commonResponse.Message = Helper.appointmentConfirmedError;
                }

            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = Helper.FailureCode;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("DeleteAppointment/{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {                
                commonResponse.Status = await _aS.Delete(appointmentId);
                commonResponse.Message = commonResponse.Status == 1 ? Helper.appointmentDeleted : Helper.somethingWentWrong;
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
