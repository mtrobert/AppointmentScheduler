using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler._Utilities
{
    public static class Helper
    {
        public static string Admin = "Admin";
        public static string Client = "Client";
        public static string Hairdresser = "Hairdresser";
        public static string appointmentAdded = "Appointment added successfully.";
        public static string appointmentUpdated = "Appointment updated successfully.";
        public static string appointmentDeleted = "Appointment deleted successfully.";
        public static string appointmentExists = "Appointment for selected date and time already exists.";
        public static string appointmentNotExists = "Appointment not exists.";
        public static string appointmentConfirmed = "Appointment confirmed successfully.";
        public static string appointmentConfirmedError = "Error!. Appointment not confirmed.";

        public static string appointmentAddError = "Something went wront, Please try again.";
        public static string appointmentUpdatError = "Something went wront, Please try again.";
        public static string somethingWentWrong = "Something went wront, Please try again.";

        public static int FailureCode = 0;
        public static int SuccessCode = 1;

        public static List<SelectListItem> DropDown_GetRoles(bool isAdmin)
        {
            if (isAdmin)
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = Helper.Admin, Text = Helper.Admin },                
                };
            }
            else
            {
                return new List<SelectListItem>
                {                    
                    new SelectListItem{Value = Helper.Hairdresser, Text = Helper.Hairdresser},
                    new SelectListItem{Value = Helper.Client, Text = Helper.Client}
                };
            }
        }

        public static List<SelectListItem> GetTimeDropDown()
        {
            int minute = 60;
            List<SelectListItem> duration = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr" });
                minute = minute + 30;
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr 30 min" });
                minute = minute + 30;
            }
            return duration;
        }
    }
}
