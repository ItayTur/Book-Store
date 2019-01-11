using System.Collections.Generic;

namespace Common.Models
{
    public class EmployeeModel: PersonModel
    {
        #region Properties
        
        public string UserName { get; set; }

        
        public string Password { get; set; }

        
        public bool IsAdmin { get; set; }


        public ICollection<PurchaseModel> Purchases { get; set; }

        #endregion


        public EmployeeModel()
        {
            Purchases = new List<PurchaseModel>();
        }
    }
}
