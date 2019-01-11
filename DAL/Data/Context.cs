using Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    /// <summary>
    /// Entity framework context class. 
    /// </summary>
    public class Context: DbContext
    {
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<AbstractBookModel> AbstractBooks { get; set; }
        public DbSet<PurchaseModel> Purchases { get; set; }
        public DbSet<LogModel> Logs { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Context(): base("Context")
        {
            // The SetInitializer method is used 
            // to configure EF to use our custom database initializer class
            // which contains our app's seed data.
            Database.SetInitializer(new DatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbstractBookModel>().HasMany(b => b.Purchases).WithOptional(p => p.AbstractBook);
            modelBuilder.Entity<BookModel>().ToTable("Books");
            modelBuilder.Entity<JournalModel>().ToTable("Journals");
        }
    }
}
