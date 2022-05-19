using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apartment.Models
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }

        public int TenantId { get; set; }

        [Required]
        [Display(Name = "Receipt Number")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Receipt Number must be numeric.")]
        public int? ReceiptNumber { get; set; }

        [Required]
        [Display(Name = "Date Posted")]
        public string DatePosted { get; set; }

        [Required]
        [Display(Name = "Bill Period")]
        public string BillPeriod { get; set; }

        [Required]
        [Display(Name = "Received By")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Received By can only contain alphabet.")]
        public string ReceivedBy { get; set; }

        [Required]
        [Display(Name = "Payment Amount")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Payment Amount must be numeric.")]
        public string PaymentAmount { get; set; }
    }
}