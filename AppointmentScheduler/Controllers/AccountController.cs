using AppointmentScheduler._Utilities;
using AppointmentScheduler.Models;
using AppointmentScheduler.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm, RoleManager<IdentityRole> rm)
        {
            _db = db;
            _userManager = um;
            _signInManager = sm;
            _roleManager = rm;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var user =await _userManager.FindByNameAsync(model.Email);
                    HttpContext.Session.SetString("ssUserName", user.Name);
                    //var ssUserName = HttpContext.Session.GetString("ssUserName");
                    return RedirectToAction("Index", "Appointment");
                }

                ModelState.AddModelError("", "Login failed, one or more details are incorrect.");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");    
        }


        public IActionResult Register()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Name = model.Name,
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleType);

                    if (!User.IsInRole(Helper.Admin))
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                    else
                    {
                        TempData["signUp_newAdmin"] = user.Name;
                    }
                    return RedirectToAction("Index", "Appointment");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }

}
