using Client.LoanApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Client.LoanApplication.OpenAPIConfiguration;
using System.Threading.Tasks;

namespace Client.LoanApplication.Controllers
{

    [Authorize(Roles = "Broker")]
    public class BrokerController : Controller
    {
        private readonly IMapper _mapper = null;
        private readonly IBrokerClient _brokerClient = null;
        public BrokerController(IMapper _mapper, IBrokerClient _brokerClient)
        {
            this._mapper = _mapper;
            this._brokerClient = _brokerClient;
        }

        /// <summary>
        /// Get all Dashboard details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Dashboard()
        {
            return View(_mapper.Map<List<Models.LoanDetails>>(await _brokerClient.GetAllLoanDetailsAsync()).ToList() as IEnumerable<Models.LoanDetails>);
        }

        /// <summary>
        /// Blank Page for Create case
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateCase(int? Id)
        {
            return View();
        }

        /// <summary>
        /// Save details
        /// </summary>
        /// <param name="_loanDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveDetails(Models.LoanDetails _loanDetails)
        {
            bool IsSuccess = false;
            dynamic showMessageString = string.Empty;

            ModelState.Remove("UWStatus");
            ModelState.Remove("Id");

            var error = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                LoanDetails _loanDetailsClnt = _mapper.Map<LoanDetails>(_loanDetails);

                // Update the case
                if (_loanDetails.Id > 0) 
                {
                    _loanDetails.ModifiedBy = User.Identity.Name;
                    IsSuccess = await _brokerClient.UpdateLoanDetailsAsync(_loanDetailsClnt);
                }
                // Add the new case 
                else
                {
                    _loanDetails.CreatedBy = User.Identity.Name;
                    IsSuccess = await _brokerClient.InsertLoanDetailsAsync(_loanDetailsClnt);
                }

                showMessageString = new
                {
                    IsSuccess = IsSuccess,
                    RedirectUrl = "/Broker/Dashboard"
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

        /// <summary>
        /// Get details for Edit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetLoanDetailsbyId(int id)
        {
            ModelState.Remove("UWStatus");
            var error = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                Models.LoanDetails _loanDetails = _mapper.Map<Models.LoanDetails>(await _brokerClient.GetLoanDetailsByIDAsync(id));

                if (_loanDetails != null && _loanDetails.Id == id)
                {
                    return View("CreateCase", _loanDetails);
                }
            }
            return View("Dashboard");
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteCase(int id)
        {
            bool IsSuccess = false;
            dynamic showMessageString = string.Empty;
            ModelState.Remove("UWStatus");
            var error = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                IsSuccess = await _brokerClient.DeleteLoanDetailsAsync(id);
            }
            showMessageString = new
            {
                IsSuccess = IsSuccess,
            };
            return Json(showMessageString, JsonRequestBehavior.AllowGet);
        }
    }
}