using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceWebsite.Data;
using eCommerceWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebsite.Controllers
{
    public class UserController : Controller
    {
        private readonly ProductContext _context;

        public UserController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                //map data to user account instance
                UserAccount userAccount = new UserAccount()
                {
                    DateOfBirth = reg.DateOfBirth,
                    Email = reg.Email,
                    Password = reg.Password,
                    Username = reg.Username
                };
                //add user to DB
                _context.UserAccounts.Add(userAccount);
                 await _context.SaveChangesAsync();

                //redirect to home page.
                return RedirectToAction("Index", "Home");
                
            }
            return View(reg);
        }
        [HttpGet]
        public IActionResult Login()
        {
            //check if user already logged in.
            if (HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            UserAccount account =
                 await (from u in _context.UserAccounts
                        where (u.Username == model.UsernameOrEmail
                            || u.Email == model.UsernameOrEmail)
                            && u.Password == model.Password
                        select u).SingleOrDefaultAsync();
            if (account == null)
            {
                ModelState.AddModelError(string.Empty, "Credentials were not found.");
                return View(model);
            }

            //Log user into website
            HttpContext.Session.SetInt32("UserId", account.UserID);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {

            //clears session data
            HttpContext.Session.Clear();

            return RedirectToAction("Index","Home");
        }
    }

}
