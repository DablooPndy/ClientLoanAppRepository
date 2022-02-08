using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.LoanApplication.OpenAPIConfiguration
{
    public class Loginclient : LoginClient, ILoginClient
    {
        public Loginclient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}