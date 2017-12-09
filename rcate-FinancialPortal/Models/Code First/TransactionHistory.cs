﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rcate_FinancialPortal.Models.Code_First
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public DateTime Created { get; set; }
        public string AuthorId { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}