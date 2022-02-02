using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [StringLength(20)]
        public string ChargeType { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        public long Contact { get; set; }

        public int Postcode { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [StringLength(20)]
        public string UWStatus { get; set; }

        [StringLength(300)]
        public string UWReason { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string CaseReviewedBy { get; set; }

        public DateTime? CaseReviewedDate { get; set; }
    }
}