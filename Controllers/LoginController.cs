using System; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Belt.Models; 
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic; 
using System.Linq; 
using Microsoft.AspNetCore.Identity; 

namespace Belt.Controllers{

    public class LoginController: Controller{

        private BeltContext _context; 
        public LoginController(BeltContext context){
            _context = context; 
        }

        [HttpGet]
        [Route("")]
        public IActionResult index(){
            return View("index");
        }

        [HttpPost]
        [Route("process")]
        public IActionResult process(UserViewModel user){
            if(ModelState.IsValid){
                User newuser = new User{
                    FirstName = user.FirstName, 
                    LastName = user.LastName, 
                    Email = user.Email, 
                    Password = user.Password
                };
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                newuser.Password = hasher.HashPassword(newuser, newuser.Password);
                _context.Add(newuser);
                _context.SaveChanges(); 
                User temp = _context.Users.Single(u => u.Email == user.Email);
                HttpContext.Session.SetInt32("ActiveUser", temp.UserId);
                return RedirectToAction("home", "Activity");
            }
            return View("index");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login(string email, string password){
            if(email == null || password == null){
                ViewBag.error = "Please fill each field";
                return View("index");
            }
            var result = _context.Users.Where(user => user.Email == email).ToList(); 
            if(result.Count == 0){
                ViewBag.error = "Email does not exist";
                return View("index");
            }
            else{
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(result[0], result[0].Password, password)){
                    User temp = _context.Users.Single(u => u.Email == email);
                    HttpContext.Session.SetInt32("ActiveUser", temp.UserId);
                    return RedirectToAction("home", "Activity");
                }
                ViewBag.error = "Password did not match";
                return View("index");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult logout(){
            HttpContext.Session.Clear(); 
            return Redirect("/");
        }

    }
}