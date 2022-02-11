using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Client.LoanApplication.CustomValidation;

namespace Client.LoanApplication.Models
{
    public class LoanDetails
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal Valuation { get; set; }
        public decimal LTV { get; set; }

        [Display(Name = "Charge Type")]
        public ChargeType ChargeType { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public Gender Gender { get; set; }
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Value for Contact must be 10 digit")]
        public long? Contact { get; set; }

        [RegularExpression(@"[0-9]{6}", ErrorMessage = "Value for Contact must be 6 digit")]
        public int? Postcode { get; set; }

        [Display(Name = "Status")]
        [Required(AllowEmptyStrings = true)]
        public UWStatus? UWStatus { get; set; }

        [RequiredIf("UWStatus", "Rejected", "The Reason field is required.")]
        [StringLength(300)]
        [Display(Name = "Reason")]
        public string UWReason { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string CaseReviewedBy { get; set; }
    }
}