namespace rcate_FinancialPortal.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using rcate_FinancialPortal.Models;
    using rcate_FinancialPortal.Models.Code_First;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<rcate_FinancialPortal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(rcate_FinancialPortal.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                roleManager.Create(new IdentityRole { Name = "Administrator" });
            }
            if (!context.Roles.Any(r => r.Name == "Demo"))
            {
                roleManager.Create(new IdentityRole { Name = "Demo" });
            }

            /////////////////////////////////////////////////////////////////////////////////////

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "ryan.cate@yahoo.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ryan.cate@yahoo.com",
                    Email = "ryan.cate@yahoo.com",
                    FirstName = "Ryan",
                    LastName = "Cate",
                }, "Password1");
            }
            var Admin1 = userManager.FindByEmail("ryan.cate@yahoo.com").Id;
            userManager.AddToRole(Admin1, "Administrator");

            if (!context.Users.Any(u => u.Email == "demo@portal.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demo@portal.com",
                    Email = "demo@portal.com",
                    FirstName = "Demo",
                    LastName = "",
                }, "Password1");
            }
            var Demo = userManager.FindByEmail("demo@portal.com").Id;
            userManager.AddToRole(Demo, "Dependent");

            /////////////////////////////////////////////////////////////////////////////////////

            if (!context.AccountType.Any(a => a.Name == "Checking"))
            {
                var type = new AccountType();
                type.Name = "Checking";
                context.AccountType.Add(type);
            }

            if (!context.AccountType.Any(a => a.Name == "Savings"))
            {
                var type = new AccountType();
                type.Name = "Savings";
                context.AccountType.Add(type);
            }

            if (!context.AccountType.Any(a => a.Name == "Retirement"))
            {
                var type = new AccountType();
                type.Name = "Retirement";
                context.AccountType.Add(type);
            }

            if (!context.AccountType.Any(a => a.Name == "Other"))
            {
                var type = new AccountType();
                type.Name = "Other";
                context.AccountType.Add(type);
            }

            /////////////////////////////////////////////////////////////////////////////////////

            if (!context.BudgetCategory.Any(b => b.Name == "Food"))
            {
                var category = new BudgetCategory();
                category.Name = "Food";
                context.BudgetCategory.Add(category);
            }

            if (!context.BudgetCategory.Any(b => b.Name == "Gas"))
            {
                var category = new BudgetCategory();
                category.Name = "Gas";
                context.BudgetCategory.Add(category);
            }

            if (!context.BudgetCategory.Any(b => b.Name == "School"))
            {
                var category = new BudgetCategory();
                category.Name = "School";
                context.BudgetCategory.Add(category);
            }

            if (!context.BudgetCategory.Any(b => b.Name == "Living"))
            {
                var category = new BudgetCategory();
                category.Name = "Living";
                context.BudgetCategory.Add(category);
            }

            if (!context.BudgetCategory.Any(b => b.Name == "Other"))
            {
                var category = new BudgetCategory();
                category.Name = "Other";
                context.BudgetCategory.Add(category);
            }

            /////////////////////////////////////////////////////////////////////////////////////

            if (!context.ExpenseCategory.Any(e => e.Name == "Food"))
            {
                var expense = new ExpenseCategory();
                expense.Name = "Food";
                context.ExpenseCategory.Add(expense);
            }

            if (!context.ExpenseCategory.Any(e => e.Name == "Gas"))
            {
                var expense = new ExpenseCategory();
                expense.Name = "Gas";
                context.ExpenseCategory.Add(expense);
            }

            if (!context.ExpenseCategory.Any(e => e.Name == "School"))
            {
                var expense = new ExpenseCategory();
                expense.Name = "School";
                context.ExpenseCategory.Add(expense);
            }

            if (!context.ExpenseCategory.Any(e => e.Name == "Living"))
            {
                var expense = new ExpenseCategory();
                expense.Name = "Living";
                context.ExpenseCategory.Add(expense);
            }

            if (!context.ExpenseCategory.Any(e => e.Name == "Other"))
            {
                var expense = new ExpenseCategory();
                expense.Name = "Other";
                context.ExpenseCategory.Add(expense);
            }
        }
    }
}
