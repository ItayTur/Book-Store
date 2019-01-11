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
    public class CustomersController : ApiController
    {

        private readonly ICustomersRepository customersRepository;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="customersRepository">The injected and implemented ICustomerRepository instance.</param>
        public CustomersController(ICustomersRepository customersRepository)
        {
            this.customersRepository = customersRepository;
        }



        /// <summary>
        /// Gets the user associated with the specified email.
        /// </summary>
        /// <param name="email">The unique email address.</param>
        /// <returns>Ok with the user instance if the email exist in the database,
        /// Not found otherwise.</returns>
        [HttpPost]
        [Route("api/Customers/GetCustomerByEmail")]
        [ResponseType(typeof(CustomerModel))]
        public IHttpActionResult PostCustomerByEmail(CustomerLoginModel customerLoginModel)
        {
            CustomerModel customer = customersRepository.GetCustomerByEmail(customerLoginModel.Email);
            IHttpActionResult httpActionResult = NotFound();

            if (customer != null)
            {
                httpActionResult = Ok(customer);
            }


            return httpActionResult;
        }


    }
}
