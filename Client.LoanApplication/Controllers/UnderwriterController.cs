using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Client.LoanApplication.Models;
using Client.LoanApplication.OpenAPIConfiguration;

namespace Client.LoanApplication.Controllers
{
    //[ValidateAntiForgeryToken]
    public class UnderwriterController : Controller
    {
        private readonly IMapper _mapper = null;
        private readonly IUnderwriterClient _underwriterClient = null;
        public UnderwriterController(IMapper _mapper, IUnderwriterClient _underwriterClient)
        {
            this._mapper = _mapper;
            this._underwriterClient = _underwriterClient;
        }

        /// <summary>
        /// Get all details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "underwriter")]
        public async Task<ActionResult> Dashboard()
        {
            List<Models.LoanDetails> _lsloanDetails = _mapper.Map<List<Models.LoanDetails>>(_underwriterClient.GetAllLoanDetailsAsync().Result.ToList());
            return View(_mapper.Map<List<Models.LoanDetails>>(await _underwriterClient.GetAllLoanDetailsAsync()).ToList() as IEnumerable<Models.LoanDetails>);
        }

        /// <summary>
        /// Get details for Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "underwriter")]
        public async Task<ActionResult> EditCase(int id)
        {
            if (ModelState.IsValid)
            {
                Models.LoanDetails _loanDetails = _mapper.Map<Models.LoanDetails>(await _underwriterClient.GetLoanDetailsByIDAsync(id));

                if (_loanDetails != null && _loanDetails.Id == id)
                {
                    return View(_loanDetails);
                }
            }
            return View("Dashboard");
        }

        /// <summary>
        /// Update Details
        /// </summary>
        /// <param name="_loanDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "underwriter")]
        public async Task<ActionResult> SaveDetails(Models.LoanDetails _loanDetails)
        {
            bool IsSuccess = false;
            dynamic showMessageString = string.Empty;

            ModelState.Remove("Contact");
            ModelState.Remove("Postcode");
            var error = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                LoanDetails _loanDetailsClnt = _mapper.Map<LoanDetails>(_loanDetails);

                if (await _underwriterClient.UpdateLoanDetailsAsync(_loanDetailsClnt))
                    return RedirectToAction("Dashboard", "Underwriter");

                showMessageString = new
                {
                    IsSuccess = IsSuccess,
                    RedirectUrl = "/Underwriter/Dashboard"
                };
                return Json(showMessageString, JsonRequestBehavior.AllowGet);
            }
            else
            {
                showMessageString = new
                {
                    IsSuccess = IsSuccess,
                };
                return Json(showMessageString, JsonRequestBehavior.AllowGet);
            }
        }
    }
}