using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rcate_FinancialPortal.Models.Code_First
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string OwnerId { get; set; }
        public int? HouseHoldId { get; set; }
        
        public virtual BudgetCategory Category { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual HouseHold HouseHold { get; set; }
    }
}