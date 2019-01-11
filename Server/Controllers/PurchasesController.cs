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
    public class PurchasesController : ApiController
    {

        private readonly IPurchasesRepository purchasesRepository;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="purchasesRepository">The injected and implamented IPurchasesRepository instance.</param>
        public PurchasesController(IPurchasesRepository purchasesRepository)
        {
            this.purchasesRepository = purchasesRepository;
        }


        [HttpPost]
        [ResponseType(typeof(PurchaseModel))]
        public IHttpActionResult Add(PurchaseModel purchase)
        {
            IHttpActionResult httpActionResult = BadRequest("PurchaseModel instance sent is not valide.");
            if (ModelState.IsValid)
            {
                var addedPurchase = purchasesRepository.Add(purchase);
                httpActionResult = Ok(addedPurchase);
            }

            return httpActionResult;
        }
    }
}
