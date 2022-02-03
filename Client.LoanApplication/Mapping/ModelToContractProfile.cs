using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Client.LoanApplication.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class ModelToContractProfile : Profile
    {
        /// <summary>
        /// Map Model To Contract
        /// </summary>
        public ModelToContractProfile()
        {
            CreateMap<Models.LoanDetails,LoanDetails>();
        }
    }
}