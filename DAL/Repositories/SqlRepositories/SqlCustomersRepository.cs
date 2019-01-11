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
    public class SqlCustomersRepository : ICustomersRepository
    {
        private readonly ILogsRepository logsRepository;


        public SqlCustomersRepository(ILogsRepository logsRepository)
        {
            this.logsRepository = logsRepository;
        }


        /// <summary>
        /// Returns a collection of all the customers entities
        /// in the database.
        /// </summary>
        /// <returns>An IEnumerable collection of EmployeeModel entity instances.</returns>
        public IEnumerable<CustomerModel> GetAll()
        {
            try
            {
                using (Context context = new Context())
                {
                    return context.Customers.ToList();
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            return null;
            
        }


        /// <summary>
        /// Gets the customer associated with the spcified ID.
        /// </summary>
        /// <param name="customerId">The unique customer ID.</param>
        /// <returns>A fully populated CustomerModel entity instance.</returns>
        public CustomerModel GetCustomerById(int customerId)
        {
            try
            {
                using (Context context = new Context())
                {
                    return context.Customers.Find(customerId);
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }

            return null;
        }


        /// <summary>
        /// Adds a customer to the database.
        /// </summary>
        /// <param name="customer">The CustomerModel entity instance to add.</param>
        /// <returns>A fully populated CustomerModel entity instance.</returns>
        public CustomerModel Add(CustomerModel customer)
        {
            try
            {
                using (Context context = new Context())
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();

                    return customer;
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }

            return null;
        }


        /// <summary>
        /// Update the customer associated with the spcified CustomerModel instance.
        /// </summary>
        /// <param name="customer">The CustomerModel entity instance to update.</param>
        public void Update(CustomerModel customer)
        {
            try
            {
                using (Context context = new Context())
                {
                    context.Customers.Attach(customer);
                    context.Entry(customer).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            
        }


        /// <summary>
        /// Removes the customer associated with the spcified ID.
        /// </summary>
        /// <param name="customerId">The unique customer ID.</param>
        public void Remove(int customerId)
        {
            try
            {
                using (Context context = new Context())
                {
                    var customerToRemove = context.Customers.Include(c=>c.Purchases).FirstOrDefault(c=>c.Id==customerId);

                    foreach (var purchase in customerToRemove.Purchases)
                    {
                        purchase.CustomerId = null;
                    }
                    context.Customers.Remove(customerToRemove);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
        }


        /// <summary>
        /// Gets the customer associated with the specidied email.
        /// </summary>
        /// <param name="email">The unique email address.</param>
        /// <returns>A fully populated instance of CustomerModel.</returns>
        public CustomerModel GetCustomerByEmail(string email)
        {
            try
            {
                using (Context context = new Context())
                {
                    return context.Customers.FirstOrDefault(c => c.Email == email);
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            return null;
            
        }
    }
}
