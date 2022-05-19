using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment.Models
{
    public class TenantTransactionViewModel
    {
        public int TenantId { get; set; }

        public string TenantName { get; set; }

        public string TenantAddress { get; set; }

        public string TenantPhoneNumber { get; set; }

        public string TenantBalance { get; set; }

        public List<TransactionViewModel> TenantTransaction { get; set; }
    }
}