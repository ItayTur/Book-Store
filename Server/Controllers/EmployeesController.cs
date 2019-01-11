using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Server.Controllers
{
    public class EmployeesController : ApiController
    {


        private readonly IEmployeesRepository employeesRepository;



        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="employeesRepository">The repository instance to be dependency injected.</param>
        public EmployeesController(IEmployeesRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;
        }


          
        [HttpGet]
        public IEnumerable<EmployeeModel> GetAll()
        {
            return employeesRepository.GetAll();
        }


        [HttpPost]
        [ResponseType(typeof(EmployeeModel))]
        [Route("api/Employees/Login")]
        public IHttpActionResult Login(UserNamePasswordModel loginModel)
        {
            EmployeeModel employee = employeesRepository.GetByUserNameAndPassword(loginModel.UserName, loginModel.Password);

            IHttpActionResult httpActionResult = BadRequest("The user name or password is incorrect");
            if(employee != null)
            {
                httpActionResult = Ok(employee);
            }

            return httpActionResult;
        }
    }
}
