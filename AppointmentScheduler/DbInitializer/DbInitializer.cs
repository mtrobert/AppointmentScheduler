using AppointmentScheduler._Utilities;
using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public DbInitializer (ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {

            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw;
            }

            if (_db.Roles.Any(x => x.Name == _Utilities.Helper.Admin)) return;
            
            _roleManager.CreateAsync(new IdentityRole(Helper.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Helper.Client)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Helper.Hairdresser)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser 
            { 
                UserName = "robert.m.1305@gmail.com",
                Email = "robert@gmail.com",
                Name = "Admin Rob",
                EmailConfirmed = true,
                

            }, "Aa@12345").GetAwaiter().GetResult();

            ApplicationUser applicationUser = _db.Users.FirstOrDefault(u => u.Email == "robert.m.1305@gmail.com");
            _userManager.AddToRoleAsync( applicationUser, Helper.Admin).GetAwaiter().GetResult();
        }
    }
}
