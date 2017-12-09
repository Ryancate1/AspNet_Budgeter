using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rcate_FinancialPortal.Models.Code_First
{
    public class Transaction
    {

        public Transaction()
        {
            Attachment = new HashSet<TransactionAttachment>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
        public decimal? ReconciledAmount { get; set; }
        public bool Reconciled { get; set; }
        public string AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public int AccountsId { get; set; }
        public int? ExpenseId { get; set; }
        public int? BudgetId { get; set; }
        public int? HouseHoldId { get; set; }
        public bool? Void { get; set; }
        
        public virtual TransactionCategory Category { get; set; }
        public virtual Accounts Accounts { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Budget Budget { get; set; }
        public virtual HouseHold HouseHold { get; set; }

        public virtual ICollection<TransactionAttachment> Attachment { get; set; }

    }
}