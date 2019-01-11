using Common.Interfaces;
using Common.Models;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.SqlRepositories
{
    /// <summary>
    /// Repository provides verious data base queries 
    /// and CRUD oprations.
    /// </summary>
    public class SqlLogsRepository: ILogsRepository
    {
        /// <summary>
        /// Adds a new log to the database.
        /// </summary>
        /// <param name="message">The log message.</param>
        public void Add(string message)
        {
           using(Context context = new Context())
            {
                var log = new LogModel
                {
                    Message = message,
                    Date = DateTime.Now
                };
                context.Logs.Add(log);
                context.SaveChanges();
            }
        }


        /// <summary>
        /// Gets all the logs in the database.
        /// </summary>
        /// <returns>A fully populated collection of LogModel entities.</returns>
        public IEnumerable<LogModel> GetAll()
        {
            using(Context context = new Context())
            {
                return context.Logs.ToList();
            }
        }

    }
}
