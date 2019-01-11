using Common.Interfaces;
using Common.Models;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories.SqlRepositories
{
    /// <summary>
    /// Repository class providing various database
    /// queries and CRUD operations.
    /// </summary>
    public class SqlAbstractBooksRepository : IAbstractBooksRepository
    {

        private readonly ILogsRepository logsRepository;

        public SqlAbstractBooksRepository(ILogsRepository logsRepository)
        {
            this.logsRepository = logsRepository;
        }

        /// <summary>
        /// Adds an abstract book to the database.
        /// </summary>
        /// <param name="abstarctBook">The AbstractBookModel entity instance to add.</param>
        /// <returns>A fully populated AbstarctBookModel entity instance.</returns>
        public AbstractBookModel Add(AbstractBookModel abstractBook)
        {
            try
            {
                using (Context context = new Context())
                {
                    context.AbstractBooks.Add(abstractBook);
                    context.SaveChanges();

                    return abstractBook;
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            return null;
        }



        /// <summary>
        /// Gets the abstract book associated with the spcified ID.
        /// </summary>
        /// <param name="abstractBookId">The unique abstractBook ID.</param>
        /// <returns>A fully populated AbstractBookModel entity instance.</returns>
        public AbstractBookModel GetAbstractBookById(int abstractBookId)
        {
            try
            {
                using (Context context = new Context())
                {
                    return context.AbstractBooks.Find(abstractBookId);
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            return null;
        }



        /// <summary>
        /// Returns a collection of all the AbstractBookModel entities
        /// in the database.
        /// </summary>
        /// <returns>An IEnumerable collection of AbstractBookModel entity instances.</returns>
        public IEnumerable<AbstractBookModel> GetAll()
        {
            try
            {
                using (Context context = new Context())
                {
                    return context.AbstractBooks.ToList();
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            return null;
        }



        /// <summary>
        /// Removes the abstract book associated with the spcified ID.
        /// </summary>
        /// <param name="abstractBookId">The unique abstarct book ID.</param>
        public void Remove(int abstractBookId)
        {
            try
            {
                using (Context context = new Context())
                {
                    var bookToRemove = context.AbstractBooks.Include(a => a.Purchases).FirstOrDefault(a => a.Id == abstractBookId);
                    foreach (var purchase in bookToRemove.Purchases)
                    {
                        purchase.AbstractBookId = null;
                    }
                    context.AbstractBooks.Remove(bookToRemove);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            
        }



        /// <summary>
        /// Update the abstract book associated with the spcified AbstractBookModel instance.
        /// </summary>
        /// <param name="abstractBook">The AbstractBookModel entity instance to update.</param>
        public void Update(AbstractBookModel abstractBook)
        {
            try
            {
                using (Context context = new Context())
                {
                    context.AbstractBooks.Attach(abstractBook);
                    context.Entry(abstractBook).State = EntityState.Modified;
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
