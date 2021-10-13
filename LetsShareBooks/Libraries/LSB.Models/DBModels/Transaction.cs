using System;
using System.Collections.Generic;
using System.Text;

namespace LSB.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public DateTime TransactionStartDate { get; set; }
        public User Publisher { get; set; }
        public User Receiver { get; set; }
        public Item Item { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
