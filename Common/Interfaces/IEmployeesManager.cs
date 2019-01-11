using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    /// <summary>
    /// Interface providing managemant operations
    /// and model validations.
    /// </summary>
    public interface IEmployeesManager
    {

        /// <summary>
        /// Logins employee into the application.
        /// </summary>
        /// <param name="userName">The employee user name.</param>
        /// <param name="password">The employee password.</param>
        /// <returns>Error message if there was an error, else returns null. </returns>
        string LoginEmployee(string userName, string password);


        /// <summary>
        /// Adds new employee if valid.
        /// </summary>
        /// <param name="employee">The employee instance to add.</param>
        /// <returns>Error message if there was an error, else returns null.</returns>
        string AddEmployee(EmployeeModel employee);


        /// <summary>
        /// Logins a customer to recive it's data.
        /// </summary>
        /// <param name="email">The customer unique email.</param>
        /// <returns>The CustomerModel associated with the specified email.</returns>
        CustomerModel LoginCustomer(string email);


        /// <summary>
        /// Adds new customer if valid.
        /// </summary>
        /// <param name="customer">The CutomerModel instance to be added.</param>
        /// <returns>Null if the CustomerModel is valid, returns error message otherwise.</returns>
        string AddCustomer(CustomerModel customer);


        /// <summary>
        /// Creates a new purchase and addes it to the database.
        /// </summary>
        /// <param name="employeeId">The selling employee unique ID.</param>
        /// <param name="customerId">The buying customer unique ID.</param>
        /// <param name="bookId">The sold book unique ID.</param>
        /// <returns>Error message if the opperation failed, else returns null.</returns>
        string Buy(int employeeId, int customerId, int bookId);

    }
}
