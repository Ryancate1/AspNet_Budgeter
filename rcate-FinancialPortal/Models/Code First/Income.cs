using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rcate_FinancialPortal.Models.Code_First
{
    public class Income
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public int IncomeTypeId { get; set; }
        public string Frequency { get; set; }

        public virtual ApplicationUser Owner { get; set; }
    }
}