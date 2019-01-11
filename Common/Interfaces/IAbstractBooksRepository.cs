using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    /// <summary>
    /// Interface providing various database
    /// queries and CRUD operations.
    /// </summary>
    public interface IAbstractBooksRepository
    {


        /// <summary>
        /// Returns a collection of all the AbstractBookModel entities
        /// in the database.
        /// </summary>
        /// <returns>An IEnumerable collection of AbstractBookModel entity instances.</returns>
        IEnumerable<AbstractBookModel> GetAll();


        /// <summary>
        /// Gets the abstract book associated with the spcified ID.
        /// </summary>
        /// <param name="abstractBookId">The unique abstractBook ID.</param>
        /// <returns>A fully populated AbstractBookModel entity instance.</returns>
        AbstractBookModel GetAbstractBookById(int abstractBookId);


        /// <summary>
        /// Adds an abstract book to the database.
        /// </summary>
        /// <param name="abstarctBook">The AbstractBookModel entity instance to add.</param>
        /// <returns>A fully populated AbstarctBookModel entity instance.</returns>
        AbstractBookModel Add(AbstractBookModel abstractBook);


        /// <summary>
        /// Update the abstract book associated with the spcified AbstractBookModel instance.
        /// </summary>
        /// <param name="abstractBook">The AbstractBookModel entity instance to update.</param>
        void Update(AbstractBookModel abstractBook);


        /// <summary>
        /// Removes the abstract book associated with the spcified ID.
        /// </summary>
        /// <param name="abstractBookId">The unique abstarct book ID.</param>
        void Remove(int abstractBookId);


    }
}
