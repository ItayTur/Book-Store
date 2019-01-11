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
    public interface ICustomersRepository
    {


        /// <summary>
        /// Returns a collection of all the customers entities
        /// in the database.
        /// </summary>
        /// <returns>An IEnumerable collection of EmployeeModel entity instances.</returns>
        IEnumerable<CustomerModel> GetAll();


        /// <summary>
        /// Gets the customer associated with the spcified ID.
        /// </summary>
        /// <param name="customerId">The unique customer ID.</param>
        /// <returns>A fully populated CustomerModel entity instance.</returns>
        CustomerModel GetCustomerById(int customerId);


        /// <summary>
        /// Adds a customer to the database.
        /// </summary>
        /// <param name="customer">The CustomerModel entity instance to add.</param>
        /// <returns>A fully populated CustomerModel entity instance.</returns>
        CustomerModel Add(CustomerModel customer);


        /// <summary>
        /// Gets the customer associated with the specidied email.
        /// </summary>
        /// <param name="email">The unique email address.</param>
        /// <returns>A fully populated instance of CustomerModel.</returns>
        CustomerModel GetCustomerByEmail(string email);


        /// <summary>
        /// Update the customer associated with the spcified CustomerModel instance.
        /// </summary>
        /// <param name="customer">The CustomerModel entity instance to update.</param>
        void Update(CustomerModel customer);


        /// <summary>
        /// Removes the customer associated with the spcified ID.
        /// </summary>
        /// <param name="customerId">The unique customer ID.</param>
        void Remove(int customerId);


    }
}
