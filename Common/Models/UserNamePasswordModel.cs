using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class UserNamePasswordModel
    {
        public readonly string UserName;

        public readonly string Password;

        public UserNamePasswordModel(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
    }
}
