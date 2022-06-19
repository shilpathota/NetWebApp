using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UseCaseInjector.Models
{
    public class UserLoginViewModel
    {
        /// <summary>
        /// User ID which is unique for each user in the database
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Username which is short ID of the user entered in the database
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The role of each user which determines the type of authentication given to them.
        /// </summary>
        public string Role { get; set; }
    }
}