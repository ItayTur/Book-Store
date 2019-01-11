using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    /// <summary>
    /// Repository provides verious data base queries 
    /// and CRUD oprations.
    /// </summary>
    public interface ILogsRepository
    {


        /// <summary>
        /// Adds a new log to the database.
        /// </summary>
        /// <param name="message">The log message.</param>
        void Add(string message);

        /// <summary>
        /// Gets all the logs in the database.
        /// </summary>
        /// <returns>A fully populated collection of LogModel entities.</returns>
        IEnumerable<LogModel> GetAll();


    }
}
