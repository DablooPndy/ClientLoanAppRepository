﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Client.LoanApplication.Models;
using Client.LoanApplication.OpenAPIConfiguration;

namespace Client.LoanApplication.Controllers
{
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
        public ActionResult Dashboard()
        {
            List<Models.LoanDetails> _lsloanDetails = _mapper.Map<List<Models.LoanDetails>>(_underwriterClient.GetAllLoanDetailsAsync().Result.ToList());

            _lsloanDetails.ForEach(s => s.LTV = Utility.GetCalculatedLTV(s.Amount, s.Valuation));

            return View(_lsloanDetails as IEnumerable<Models.LoanDetails>);
        }

        /// <summary>
        /// Get details for Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCase(int id)
        {
            if (ModelState.IsValid)
            {
                Models.LoanDetails _loanDetails = _mapper.Map<Models.LoanDetails>(_underwriterClient.GetLoanDetailsByIDAsync(id).Result);

                if (_loanDetails != null && _loanDetails.Id == id)
                {
                    _loanDetails.LTV = Utility.GetCalculatedLTV(_loanDetails.Amount, _loanDetails.Valuation);
                    return View(_loanDetails);
                }
            }
            return RedirectToAction("Dashboard", "Underwriter");
        }

        /// <summary>
        /// Update Details
        /// </summary>
        /// <param name="_loanDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCase(Models.LoanDetails _loanDetails)
        {
            if (ModelState.IsValid)
            {
                LoanDetails _loanDetailsClnt = _mapper.Map<LoanDetails>(_loanDetails);

                if (_underwriterClient.UpdateLoanDetailsAsync(_loanDetailsClnt).Result)
                    return RedirectToAction("Dashboard", "Underwriter");
            }
            return View(_loanDetails);
        }
    }
}