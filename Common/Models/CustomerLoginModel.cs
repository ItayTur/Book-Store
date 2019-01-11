using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class CustomerLoginModel
    {

        public string Email { get; }

        public CustomerLoginModel(string email)
        {
            Email = email;
        }

    }
}
