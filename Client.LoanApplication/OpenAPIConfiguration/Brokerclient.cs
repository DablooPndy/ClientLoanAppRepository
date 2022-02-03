using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.LoanApplication.OpenAPIConfiguration
{
    public class Brokerclient : BrokerClient, IBrokerClient
    {
        public Brokerclient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}