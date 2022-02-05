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
                bool IsValidUser = false;
                if (_login.UserName == "broker" && _login.Password == "broker")
                { IsValidUser = true; }
                else if (_login.UserName == "underwriter" && _login.Password == "underwriter")
                { IsValidUser = true; }

                if (IsValidUser)
                {
                    // Set Cookie for user with Non-Persistent Cookie
                    FormsAuthentication.SetAuthCookie(_login.UserName, false);

                    FormsAuthentication.SetAuthCookie(_login.UserName, false);

                    var authTicket = new FormsAuthenticationTicket(1, _login.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, _login.UserName);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);
                   
                    return _login.UserName == "broker" ?   RedirectToAction("Dashboard","Broker") : RedirectToAction("Dashboard","Underwriter");
                }

                ModelState.AddModelError("", "invalid Username or Password");
                return View(_login);
            }
            return View(_login);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}