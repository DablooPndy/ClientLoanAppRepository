using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.LoanApplication.Models;

namespace Client.LoanApplication.Controllers
{
    public class UnderwriterController : Controller
    {
       [HttpGet]
        public ActionResult Dashboard()
        {
            return View(GetAllLoandeatils());
        }

        private IEnumerable<Models.LoanDetails> GetAllLoandeatils()
        {
            var lsloanDetails = new List<Client.LoanApplication.Models.LoanDetails>();
            lsloanDetails.Add(new Client.LoanApplication.Models.LoanDetails { Id = 1, Amount = 1000, Valuation = 500, ChargeType = ChargeType.First, FirstName = "Abcfn", LastName = "Abcln", Gender = Gender.Male, Contact = 9892804840, Postcode = 421605 });
            lsloanDetails.Add(new Client.LoanApplication.Models.LoanDetails { Id = 2, Amount = 3000, Valuation = 1500, ChargeType = ChargeType.First, FirstName = "Xyzfn", LastName = "Xyzln", Gender = Gender.Female, Contact = 9892123456, Postcode = 427894 });

            return lsloanDetails as IEnumerable<Models.LoanDetails>;
        }

        [HttpGet]
        public ActionResult EditCase()
        {
            Models.LoanDetails loanDetails = new Models.LoanDetails { Id = 1, Amount = 1000, Valuation = 500, ChargeType = ChargeType.First, FirstName = "Abcfn", LastName = "Abcln", Gender = Gender.Male, Contact = 9892804840, Postcode = 421605 };
            return View(loanDetails);
        }
    }
}