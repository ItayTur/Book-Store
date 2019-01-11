using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class CustomerModel: PersonModel
    {

        #region Properties

        public CategoryEnum FavoriteCategory { get; set; }

        public ICollection<PurchaseModel> Purchases { get; set; }

        #endregion


        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomerModel()
        {
            Purchases = new List<PurchaseModel>();
        }

    }
}
