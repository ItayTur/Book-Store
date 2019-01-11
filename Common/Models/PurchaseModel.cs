using System;
using System.Data.SqlTypes;

namespace Common.Models
{
    public class PurchaseModel
    {

        #region Properties

        public int Id { get; set; }

        
        public SqlDateTime Date { get; set; }


        
        public double TotalPrice { get; set; }


        //Forgin keys properties
        public int? AbstractBookId { get; set; }

        public int? CustomerId { get; set; }

        public int? EmployeeId { get; set; }


        //Nevigation properties
        public AbstractBookModel AbstractBook { get; set; }

        public CustomerModel Customer { get; set; }

        public EmployeeModel Employee { get; set; }


        #endregion

    }
}
