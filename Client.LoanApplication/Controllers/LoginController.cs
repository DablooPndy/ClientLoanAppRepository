using Client.LoanApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            }
            return View();
        }
    }
}