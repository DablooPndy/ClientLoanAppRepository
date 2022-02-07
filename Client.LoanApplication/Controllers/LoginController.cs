using Client.LoanApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Client.LoanApplication.Controllers
{
    public class LoginController : Controller
    {

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login _login)
        {
            if (ModelState.IsValid)
            {
                string Roles = "";

                bool IsValidUser = false;
                if (_login.UserName == "Broker" && _login.Password == "broker")
                { 
                    IsValidUser = true;
                    Roles = "broker";
                }
                else if (_login.UserName == "Underwriter" && _login.Password == "underwriter")
                { 
                    IsValidUser = true;
                    Roles = "underwriter";
                }

                if (IsValidUser)
                {
                    // Set Cookie for user with Non-Persistent Cookie (Persist in Browser)
                    FormsAuthentication.SetAuthCookie(_login.UserName, false);

                    var authTicket = new FormsAuthenticationTicket(1, _login.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, Roles);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    return _login.UserName == "Broker" ?   RedirectToAction("Dashboard","Broker") : RedirectToAction("Dashboard","Underwriter");
                }

                ModelState.AddModelError("", "invalid Username or Password");
                //return ("Login", _login);
            }
            return View ("Login",_login);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
