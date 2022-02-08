using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Client.LoanApplication.Models
{
    public class Login
    {
        [StringLength(100)]
        [Display(Name ="User Name")]
        [Required(ErrorMessage ="required")]
       
        public string UserName { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Roles { get; set; }
        public bool IsUserValid { get; set; }
    }
}