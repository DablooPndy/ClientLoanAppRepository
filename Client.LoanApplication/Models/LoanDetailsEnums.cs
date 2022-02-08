using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Client.LoanApplication.Models
{
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
    public enum ChargeType
    {
        First = 1,
        Second = 2
    }
    public enum UWStatus
    {
        Approved = 1,
        Rejected = 2,
        Cancelled = 3
    }
}