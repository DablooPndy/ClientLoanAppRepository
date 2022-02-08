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
    public class ContractToModelProfile : Profile
    {
        /// <summary>
        /// Map Contract To Model
        /// </summary>
        public ContractToModelProfile()
        {
            CreateMap<LoanDetails, Models.LoanDetails>();//.ForMember(i => i.UWStatus,s => s.Ignore());
            CreateMap<LoginDetails, Models.Login>();
        }
    }
}