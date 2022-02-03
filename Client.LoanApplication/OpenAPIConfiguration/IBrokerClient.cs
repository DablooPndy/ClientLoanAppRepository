using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.LoanApplication.OpenAPIConfiguration
{
    public interface IBrokerClient
    {
        Task<ICollection<LoanDetails>> GetAllLoanDetailsAsync();
        Task<LoanDetails> GetLoanDetailsByIDAsync(int loanId);
        Task<bool> InsertLoanDetailsAsync(LoanDetails _loanDetails);
        Task<bool> UpdateLoanDetailsAsync(LoanDetails _loanDetails);
        Task<bool> DeleteLoanDetailsAsync(int loanId);
    }
}
