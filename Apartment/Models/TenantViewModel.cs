using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apartment.Models
{
    public class TenantViewModel
    {
        public int TenantId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Name can only contain alphabet.")]
        public string TenantName { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(100, ErrorMessage = "Address Field exceeded 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9 ,.'#]+$", ErrorMessage = "Address can only contain Alphanumeric characters.")]
        public string TenantAddress { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "The {0} must be at least {2} digits long", MinimumLength = 11)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Phonenumber must be numeric.")]
        [Display(Name = "Phonenumber")]
        public string TenantPhoneNumber { get; set; }

        [Display(Name = "Balance")]
        public string TenantBalance { get; set; }

   
    }
}