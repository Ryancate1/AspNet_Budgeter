using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rcate_FinancialPortal.Models.Code_First
{
    public class Accounts
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Opened { get; set; }
        public DateTime? Updated { get; set; }
        public string OwnerId { get; set; }
        [Required]
        public int AccountTypeId { get; set; }
        [Required]
        public decimal Balance { get; set; }
        public int? HouseHoldId { get; set; }

        public virtual AccountType AccountType { get; set; }
        public virtual HouseHold HouseHold { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        
    }
}