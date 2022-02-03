using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.LoanApplication.OpenAPIConfiguration
{
    public interface IUnderwriterClient
    {
        Task<ICollection<LoanDetails>> GetAllLoanDetailsAsync();
        Task<LoanDetails> GetLoanDetailsByIDAsync(int loanId);
        Task<bool> UpdateLoanDetailsAsync(LoanDetails _loanDetails);
    }
}
