using Common.Enums;
using System;
using System.Collections.Generic;


namespace Common.Models
{
    public abstract class AbstractBookModel
    {
        #region Properties

        public int Id { get; set; }
        

        public string Name { get; set; }

        
        public string Publisher { get; set; }

        
        public DateTime PublishDate { get; set; }


        
        public double Price { get; set; }


        
        public int Amount { get; set; }


        public CategoryEnum Category { get; set; }


        public double DiscountPercentage { get; set; }


        public ICollection<PurchaseModel> Purchases { get; set; }

        #endregion


        /// <summary>
        /// Constructor.
        /// </summary>
        public AbstractBookModel()
        {
            Purchases = new List<PurchaseModel>();
        }
        

    }
}
