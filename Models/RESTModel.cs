using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// The namespace contains all the models class for MQ, REST and UserLoginView
/// </summary>
namespace UseCaseInjector.Models
{
    public class RESTModel
    {
        /// <summary>
        /// Environment dropdown value which is assigned with the value entered by user in REST view
        /// </summary>
        public string Environment { get; set; }
        /// <summary>
        /// Producer dropdown value which is assigned with the value entered by user in REST view
        /// </summary>
        public string Producer { get; set; }
        /// <summary>
        /// Region dropdown value which is assigned with the value entered by user in REST view
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// Usecase dropdown value which is assigned with the value entered by user in REST view
        /// </summary>
        public string Usecase { get; set; }
        /// <summary>
        /// VIN value which is assigned with the value entered by user in REST view
        /// </summary>
        public string VIN { get; set; }
        /// <summary>
        /// Tracking ID value which is assigned with the value entered by user in REST view. This field is not mandatory.
        /// </summary>
        public string TrackingID { get; set; }
        /// <summary>
        /// Input Data value which is assigned with the value entered by user in REST view
        /// </summary>
        public string InputData { get; set; }
        /// <summary>
        /// Object of UserLoginViewModel which gives the values of the user logged in.
        /// </summary>
        public UserLoginViewModel userlogindetails { get; set; }
    }
}