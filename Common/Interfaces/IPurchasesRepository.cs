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
    public interface IPurchasesRepository
    {


        /// <summary>
        /// Returns a collection of all the purchases entities
        /// in the database.
        /// </summary>
        /// <returns>An IEnumerable collection of PurchaseModel entity instances.</returns>
        IEnumerable<PurchaseModel> GetAll();


        /// <summary>
        /// Gets the purchase associated with the spcified ID.
        /// </summary>
        /// <param name="purchaseId">The unique purchase ID.</param>
        /// <returns>A fully populated PurchaseModel entity instance.</returns>
        PurchaseModel GetPurchaseById(int purchaseId);


        /// <summary>
        /// Adds a purchase to the database.
        /// </summary>
        /// <param name="purchase">The PurchaseModel entity instance to add.</param>
        /// <returns>A fully populated PurchaseModel entity instance.</returns>
        PurchaseModel Add(PurchaseModel purchase);


        /// <summary>
        /// Update the purchase associated with the spcified PurchaseModel instance.
        /// </summary>
        /// <param name="purchase">The PurchaseModel entity instance to update.</param>
        void Update(PurchaseModel purchase);


        /// <summary>
        /// Removes the purchase associated with the spcified ID.
        /// </summary>
        /// <param name="purchaseId">The unique purchase ID.</param>
        void Remove(int purchaseId);


    }
}
