using Common.Enums;
using Common.Models;
using System;
using System.Data.Entity;

namespace DAL.Data
{
    internal class DatabaseInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            //This is the database's seed data:
            // 2 employees, 2 customers, 2 books, 1 journal, 1 purchase.
            EmployeeModel employee1 = new EmployeeModel
            {
                FirstName = "Itay",
                LastName = "Tur",
                PhoneNumber = "1111111111",
                Address = "MyTown, MyStreet 45",
                Email = "Itay@gmail.com",
                UserName = "ItayKing",
                Password = "1",
                IsAdmin = true
            };

            EmployeeModel employee2 = new EmployeeModel
            {
                FirstName = "Avi",
                LastName = "Hadad",
                Email = "Avi@gmail.com",
                Address = "AviTown, AviStreet 3",
                PhoneNumber = "222222222",
                UserName = "AviKing",
                Password = "1",
                IsAdmin = false
            };

            CustomerModel customer1 = new CustomerModel
            {
                FirstName = "Oded",
                LastName = "Bartov",
                Email = "Oded@gmail.com",
                Address = "OdedTown, OdedStreet 3",
                PhoneNumber = "333333333",
                FavoriteCategory = CategoryEnum.Computers
            };

            CustomerModel customer2 = new CustomerModel
            {
                FirstName = "Tomer",
                LastName = "Towina",
                Email = "Tomer@gmail.com",
                Address = "TomerTown, TomerStreet 2",
                PhoneNumber = "444444444",
                FavoriteCategory = CategoryEnum.Travling
            };

            BookModel book1 = new BookModel
            {
                Name = "Israel Roads",
                Author = "Yarin Tolev",
                Amount = 20,
                Category = CategoryEnum.Travling,
                Edition = 1,
                Price = 20,
                PublishDate = DateTime.Now,
                Publisher = "Omer C Books",
                Summery = "The simplest guide for the israely travler.",
                DiscountPercentage = 50
            };

            BookModel book2 = new BookModel
            {
                Name = "Ridculas it is!!!",
                Author = "Itamr Nir",
                Amount = 23,
                Category = CategoryEnum.Comedy,
                Edition = 5,
                Price = 40,
                PublishDate = DateTime.Now,
                Publisher = "Omer C Books",
                Summery = "Funny jokes for funny conversation.",
                DiscountPercentage = 50
            };

            JournalModel journal1 = new JournalModel
            {
                Name = "Funny Jokes",
                Amount = 23,
                Category = CategoryEnum.Comedy,
                Price = 40,
                PublishDate = DateTime.Now,
                Publisher = "Omer C Books",
                Issue = 1,
                Writer = "Oleg Mashsky",
                DiscountPercentage = 50
            };

            JournalModel journal2 = new JournalModel
            {
                Name = "Find your way!",
                Amount = 23,
                Category = CategoryEnum.Travling,
                Price = 40,
                PublishDate = DateTime.Now,
                Publisher = "Omer C Books",
                Issue = 1,
                Writer = "Oleg Mashsky",
                DiscountPercentage = 50
            };

            PurchaseModel purchase1 = new PurchaseModel
            {
                Date = DateTime.Now,
                TotalPrice = 50,
                AbstractBookId = 1,
                Customer = customer1,
                Employee = employee1
            };

            LogModel log1 = new LogModel
            {
                Message = "Test log message",
                Date = DateTime.Now
            };
            customer1.Purchases.Add(purchase1);
            employee1.Purchases.Add(purchase1);

            context.AbstractBooks.Add(book1);
            context.AbstractBooks.Add(book2);
            context.AbstractBooks.Add(journal1);
            context.AbstractBooks.Add(journal2);

            context.Employees.Add(employee1);
            context.Employees.Add(employee2);

            context.Customers.Add(customer1);
            context.Customers.Add(customer2);

            context.Purchases.Add(purchase1);

            context.Logs.Add(log1);

            context.SaveChanges();

        }
    }
}