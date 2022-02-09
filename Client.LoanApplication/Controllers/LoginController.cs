using AutoMapper;
using Client.LoanApplication.Models;
using Client.LoanApplication.OpenAPIConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Client.LoanApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMapper _mapper = null;
        private readonly ILoginClient _loginClient = null;
        public LoginController(IMapper _mapper, ILoginClient _loginClient)
        {
            this._mapper = _mapper;
            this._loginClient = _loginClient;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Models.Login _login)
        {
            if (ModelState.IsValid)
            {
                _login.Password = Utility.Encrypt(_login.Password);
                Models.Login login =  _mapper.Map<Models.Login>(await _loginClient.ValidateUserAsync(_mapper.Map<LoginDetails>(_login)));

                if (login.IsUserValid)
                {
                    // Set Cookie for user with Non-Persistent Cookie (Persist in Browser)
                    FormsAuthentication.SetAuthCookie(_login.UserName, false);

                    var authTicket = new FormsAuthenticationTicket(1, _login.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, login.Roles);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    return _login.UserName == "Broker" ? RedirectToAction("Dashboard", "Broker") : RedirectToAction("Dashboard", "Underwriter");
                }

                ModelState.AddModelError("", "invalid Username or Password");
            }
            return View("Login", _login);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
