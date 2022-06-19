using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UseCaseInjector.Models
{
    /// <summary>
    /// MQ Model class which stores the values of the fields entered by user in MQ view 
    /// </summary>
    public class MQModel
    {
        /// <summary>
        /// Environment dropdown value which is assigned with the value entered by user in MQ view
        /// </summary>
        public string Environment { get; set; }
        /// <summary>
        /// Market dropdown value which is assigned with the value entered by user in MQ view
        /// </summary>
        public string Market { get; set; }
        /// <summary>
        /// Source dropdown value which is assigned with the value entered by user in MQ view
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Application dropdown value which is assigned with the value entered by user in MQ view
        /// </summary>
        public string Application { get; set; }
        /// <summary>
        /// Input file data value which is assigned with the value entered by user in MQ view
        /// </summary>
        public string inputfileData { get; set; }
        /// <summary>
        /// Object of UserLoginViewModel which gives the values of the user logged in.
        /// </summary>
        public UserLoginViewModel userlogindetails { get; set; }

    }
}