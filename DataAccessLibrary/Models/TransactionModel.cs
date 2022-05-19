using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }

        public int TenantId  { get; set; }

        public int ReceiptNumber { get; set; }

        public string DatePosted { get; set; }

        public string BillPeriod { get; set; }

        public string ReceivedBy { get; set; }

        public int PaymentAmount { get; set; }

    }
}
