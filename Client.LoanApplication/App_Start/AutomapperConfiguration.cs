using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace Client.LoanApplication
{
    /// <summary>
    /// Automapper Configuration
    /// </summary>
    public static class AutomapperConfiguration
    {
        /// <summary>
        /// Load profiles to configure auto mapper
        /// </summary>
        /// <returns>Returns configuration  mapper</returns>
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Mapping.ContractToModelProfile());
                cfg.AddProfile(new Mapping.ModelToContractProfile());
            });
            return config.CreateMapper();
        }
    }
}