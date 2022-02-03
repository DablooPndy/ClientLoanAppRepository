using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.LoanApplication.OpenAPIConfiguration
{
    public class Underwriterclient : UnderwriterClient, IUnderwriterClient
    {
        public Underwriterclient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}