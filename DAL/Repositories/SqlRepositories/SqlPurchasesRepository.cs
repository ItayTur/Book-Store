using Common.Interfaces;
using Common.Models;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.SqlRepositories
{
    /// <summary>
    /// Repository class providing verious database
    /// queries and CRUD operations.
    /// </summary>
    public class SqlPurchasesRepository: IPurchasesRepository
    {

        private readonly ILogsRepository logsRepository;


        public SqlPurchasesRepository(ILogsRepository logsRepository)
        {
            this.logsRepository = logsRepository;
        }


        /// <summary>
        /// Returns a collection of all the purchases entities
        /// in the database.
        /// </summary>
        /// <returns>An IEnumerable collection of PurchaseModel entity instances.</returns>
        public IEnumerable<PurchaseModel> GetAll()
        {
            try
            {
                using (Context context = new Context())
                {
                    return context.Purchases.ToList();
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            return null;
        }


        /// <summary>
        /// Gets the purchase associated with the spcified ID.
        /// </summary>
        /// <param name="purchaseId">The unique purchase ID.</param>
        /// <returns>A fully populated PurchaseModel entity instance.</returns>
        public PurchaseModel GetPurchaseById(int purchaseId)
        {
            try
            {
                using (Context context = new Context())
                {
                    return context.Purchases.Include(p => p.AbstractBook).Include(p => p.Customer).Include(p => p.Employee).FirstOrDefault(p => p.Id == purchaseId);
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            return null;
        }


        /// <summary>
        /// Adds a purchase to the database.
        /// </summary>
        /// <param name="purchase">The PurchaseModel entity instance to add.</param>
        /// <returns>A fully populated PurchaseModel entity instance.</returns>
        public PurchaseModel Add(PurchaseModel purchase)
        {

            try
            {
                using (Context context = new Context())
                {
                    AbstractBookModel book = context.AbstractBooks.Find(purchase.AbstractBookId);

                    purchase.TotalPrice = (book.DiscountPercentage / 100) * book.Price;

                    context.Purchases.Add(purchase);
                    context.SaveChanges();


                    return context.Purchases.Include(p => p.AbstractBook).Include(p => p.Customer).Include(p => p.Employee).FirstOrDefault(p => p.Id == purchase.Id);
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }

            return null;
           
        }


        /// <summary>
        /// Update the purchase associated with the spcified PurchaseModel instance.
        /// </summary>
        /// <param name="purchase">The PurchaseModel entity instance to update.</param>
        public void Update(PurchaseModel purchase)
        {

            try
            {
                using (Context context = new Context())
                {
                    context.Purchases.Attach(purchase);
                    context.Entry(purchase).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
        }


        /// <summary>
        /// Removes the purchase associated with the spcified ID.
        /// </summary>
        /// <param name="purchaseId">The unique purchase ID.</param>
        public void Remove(int purchaseId)
        {
            try
            {
                using (Context context = new Context())
                {
                    var purchaseToRemove = context.Purchases.Find(purchaseId);
                    context.Purchases.Remove(purchaseToRemove);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
        }


    }
}
