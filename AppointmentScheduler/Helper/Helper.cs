using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Helper
{
    public static class Helper
    {
        private static string Admin = "Admin";
        private static string Client = "Client";
        private static string Hairdresser = "Hairdresser";

        public static List<SelectListItem> DropDown_GetRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value = Helper.Admin, Text = Helper.Admin},
                new SelectListItem{Value = Helper.Hairdresser, Text = Helper.Hairdresser},
                new SelectListItem{Value = Helper.Client, Text = Helper.Client}
            };
        }
    }
}
