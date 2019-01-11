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
    /// Repository class providing various database
    /// queries and CRUD operations.
    /// </summary>
    public class SqlEmployeesRepository: IEmployeesRepository
    {
        private readonly ILogsRepository logsRepository;

        public SqlEmployeesRepository(ILogsRepository logsRepository)
        {
            this.logsRepository = logsRepository;
        }

        /// <summary>
        /// Returns a collection of all the employees entities
        /// in the database.
        /// </summary>
        /// <returns>An IEnumerable collection of EmployeeModel entity instances.</returns>
        public IEnumerable<EmployeeModel> GetAll()
        {
            try
            {
                using (Context context = new Context())
                {
                    return context.Employees.ToList();
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            return null;
            
        }



        /// <summary>
        /// Gets the employee associated with the spcified ID.
        /// </summary>
        /// <param name="employeeId">The unique employee ID.</param>
        /// <returns>A fully populated EmployeeModel entity instance.</returns>
        public EmployeeModel GetEmployeeById(int employeeId)
        {
            try
            {
                using (Context context = new Context())
                {
                    return context.Employees.Find(employeeId);
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            return null;
        }



        /// <summary>
        /// Adds an employee to the database.
        /// </summary>
        /// <param name="employee">The EmployeeModel entity instance to add.</param>
        /// <returns>A fully populated EmployeeModel entity instance.</returns>
        public EmployeeModel Add(EmployeeModel employee)
        {
            try
            {
                using (Context context = new Context())
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();

                    return employee;
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
            return null;
        }



        /// <summary>
        /// Update the employee associated with the spcified EmployeeModel instance.
        /// </summary>
        /// <param name="employee">The EmployeeModel entity instance to update.</param>
        public void Update(EmployeeModel employee)
        {
            try
            {
                using (Context context = new Context())
                {
                    context.Employees.Attach(employee);
                    context.Entry(employee).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
        }



        /// <summary>
        /// Removes the employee associated with the spcified ID.
        /// </summary>
        /// <param name="enployeeId">The unique employee ID.</param>
        public void Remove(int employeeId)
        {
            try
            {
                using (Context context = new Context())
                {
                    var employeeToRemove = context.Employees.Find(employeeId);

                    foreach (var purchase in employeeToRemove.Purchases)
                    {
                        purchase.EmployeeId = null;
                    }

                    context.Employees.Remove(employeeToRemove);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                logsRepository.Add(e.Message);
            }
        }



        /// <summary>
        /// Gets the employee associated with the specified user name and password.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>EmployeeModel instance if finds a user with the same password, elsewise returns null.</returns>
        public EmployeeModel GetByUserNameAndPassword(string userName, string password)
        {
            try
            {
                using (Context context = new Context())
                {
                    var employeeToReturn = context.Employees.FirstOrDefault(e => e.UserName == userName && e.Password == password);
                    return employeeToReturn;
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
