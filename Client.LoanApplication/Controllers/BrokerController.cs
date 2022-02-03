﻿using Client.LoanApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Client.LoanApplication.OpenAPIConfiguration;

namespace Client.LoanApplication.Controllers
{
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
        public ActionResult Dashboard()
        {
            List<Models.LoanDetails> _lsloanDetails = _mapper.Map<List<Models.LoanDetails>>(_brokerClient.GetAllLoanDetailsAsync().Result.ToList());

            _lsloanDetails.ForEach(s => s.LTV = Utility.GetCalculatedLTV(s.Amount,s.Valuation));

            return View(_lsloanDetails as IEnumerable<Models.LoanDetails>);
        }

        /// <summary>
        /// Blank Page for Create case
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateCase()
        {
            return View();
        }

        /// <summary>
        /// Add New Case
        /// </summary>
        /// <param name="_loanDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCase(Models.LoanDetails _loanDetails)
        {
            if (ModelState.IsValid)
            {
                LoanDetails _loanDetailsClnt = _mapper.Map<LoanDetails>(_loanDetails);

                if (_brokerClient.InsertLoanDetailsAsync(_loanDetailsClnt).Result)
                    RedirectToAction("Dashboard");
            }
            return View(_loanDetails);
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
                Models.LoanDetails _loanDetails = _mapper.Map<Models.LoanDetails>(_brokerClient.GetLoanDetailsByIDAsync(id).Result);

                if (_loanDetails != null && _loanDetails.Id == id)
                {
                    _loanDetails.LTV = Utility.GetCalculatedLTV(_loanDetails.Amount, _loanDetails.Valuation);
                    return View("CreateCase", _loanDetails);
                } 
            }
            return RedirectToAction("Dashboard");
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

                if (_brokerClient.UpdateLoanDetailsAsync(_loanDetailsClnt).Result)
                    RedirectToAction("Dashboard");
            }
            return View(_loanDetails);
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteCase(int id)
        {
            if (ModelState.IsValid)
            {
                bool isDeleted = _brokerClient.DeleteLoanDetailsAsync(id).Result;
            }
            return View();
        }
    }
}