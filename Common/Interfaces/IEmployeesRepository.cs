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
    public interface IEmployeesRepository
    {


        /// <summary>
        /// Returns a collection of all the employees entities
        /// in the database.
        /// </summary>
        /// <returns>An IEnumerable collection of EmployeeModel entity instances.</returns>
        IEnumerable<EmployeeModel> GetAll();


        /// <summary>
        /// Gets the employee associated with the spcified ID.
        /// </summary>
        /// <param name="employeeId">The unique employee ID.</param>
        /// <returns>A fully populated EmployeeModel entity instance.</returns>
        EmployeeModel GetEmployeeById(int employeeId);



        /// <summary>
        /// Gets the employee associated with the specified userName and password.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>EmployeeModel instance if finds a user with the same password, elsewise returns null.</returns>
        EmployeeModel GetByUserNameAndPassword(string userName, string password);


        /// <summary>
        /// Adds an employee to the database.
        /// </summary>
        /// <param name="employee">The EmployeeModel entity instance to add.</param>
        /// <returns>A fully populated EmployeeModel entity instance.</returns>
        EmployeeModel Add(EmployeeModel employee);


        /// <summary>
        /// Update the employee associated with the spcified EmployeeModel instance.
        /// </summary>
        /// <param name="employee">The EmployeeModel entity instance to update.</param>
        void Update(EmployeeModel employee);


        /// <summary>
        /// Removes the employee associated with the spcified ID.
        /// </summary>
        /// <param name="enployeeId">The unique employee ID.</param>
        void Remove(int employeeId);


    }
}
