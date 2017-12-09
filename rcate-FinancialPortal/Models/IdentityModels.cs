using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using rcate_FinancialPortal.Models.Code_First;
using System.Collections;
using System.Collections.Generic;

namespace rcate_FinancialPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int? HouseHoldId { get; set; }
        public virtual HouseHold HouseHold { get; set; }

        public string TimeZone { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public ApplicationUser()
        {
            Accounts = new HashSet<Accounts>();
            Transaction = new HashSet<Transaction>();
            Notification = new HashSet<Notification>();
            Attachment = new HashSet<TransactionAttachment>();
            Budget = new HashSet<Budget>();
        }

        public virtual ICollection<Accounts> Accounts { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<TransactionAttachment> Attachment { get; set; }
        public virtual ICollection<Budget> Budget { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("HouseHoldId", HouseHoldId.ToString()));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<BudgetCategory> BudgetCategory { get; set; }
        //public DbSet<Expense> Expense { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategory { get; set; }
        public DbSet<HouseHold> HouseHold { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionCategory> TransactionCategory { get; set; }
    }
}