using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.LoanApplication.OpenAPIConfiguration
{
    public interface ILoginClient
    {
        Task<LoginDetails> ValidateUserAsync(LoginDetails _loginDetails);
    }
}
