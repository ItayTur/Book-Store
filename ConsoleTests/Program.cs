using Common.Enums;
using Common.Models;
using DAL.Repositories.SqlRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var logsRepository = new SqlLogsRepository();

            #region SqlLogsRepository

            SqlAbstractBooksRepository booksRepo = new SqlAbstractBooksRepository(logsRepository);

            booksRepo.Add(null);

            var logs = logsRepository.GetAll();

            foreach (var log in logs)
            {
                Console.WriteLine(log.Message);
            }

            Console.Read();

            #endregion

            #region SqlAbstractBooksRepository Tests

            SqlAbstractBooksRepository booksRepository = new SqlAbstractBooksRepository(logsRepository);

            Console.WriteLine("Sql abstract books repository tests:");
            Console.WriteLine("============================================");
            //Checks GetAll function.
            var books = booksRepository.GetAll();
            foreach (var book in books)
            {
                Console.WriteLine(book.Name);
            }


            //Checks GetAbstractBookById function for both book and journal models.
            var bookToGet1 = booksRepository.GetAbstractBookById(1) as BookModel;
            Console.WriteLine("The book with id-1 is:  " + bookToGet1.Name);
            Console.WriteLine("The author is: "+bookToGet1.Author);

            var bookToGet2 = booksRepository.GetAbstractBookById(3) as JournalModel;
            Console.WriteLine("The journal with id-3 is:  " + bookToGet2.Name);
            Console.WriteLine("The writer is: " + bookToGet2.Writer);



            //Checks Add function.
            var bookToAdd = new BookModel
            {
                Name = "addedBook",
                Author = "Itamr Nir",
                Amount = 23,
                Category = CategoryEnum.Comedy,
                Edition = 5,
                Price = 40,
                PublishDate = DateTime.Now,
                Publisher = "Omer C Books",
                Summery = "The added book."
            };

            bookToAdd = booksRepository.Add(bookToAdd) as BookModel;
            var addedBook = booksRepository.GetAbstractBookById(bookToAdd.Id);

            Console.WriteLine("The added book is: "+addedBook.Name);


            Console.WriteLine("============================================");

            #endregion

            #region SqlPurchasesRepository

            Console.WriteLine("Sql purchases repository tests:");
            Console.WriteLine("============================================");

            SqlPurchasesRepository purchasesRepository = new SqlPurchasesRepository(logsRepository);
            
            //Checks Add function.
            PurchaseModel purchaseToAdd = new PurchaseModel
            {
                Date = DateTime.Now,
                TotalPrice = 50,
                AbstractBookId = 1,
                CustomerId = 1,
                EmployeeId = 1
            };

            var purchesAdded = purchasesRepository.Add(purchaseToAdd);
            Console.WriteLine($"The added purchase details:\n Seller: {purchesAdded.Employee.FirstName}, Buyier: {purchesAdded.Customer.FirstName}, Book: {purchesAdded.AbstractBook.Name}");

            //Checks cascade delete is off
            booksRepository.Remove(1);
            purchesAdded = purchasesRepository.GetPurchaseById(purchesAdded.Id);
            Console.WriteLine($"The purchase details after the book removal:\n Seller: {purchesAdded.Employee.FirstName}, Buyier: {purchesAdded.Customer.FirstName}, Book: {purchesAdded.AbstractBook}");

            var customersRepository = new SqlCustomersRepository(logsRepository);
            customersRepository.Remove(1);
            purchesAdded = purchasesRepository.GetPurchaseById(purchesAdded.Id);
            Console.WriteLine($"The purchase details after the customer removal:\n Seller: {purchesAdded.Employee.FirstName}, Buyier: {purchesAdded.Customer}, Book: {purchesAdded.AbstractBook}");
            Console.WriteLine("============================================");

            #endregion

            Console.Read();
        }
    }
}
