using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using UseCaseInjector.Models;
using UseCaseInjector.Global;
using System.Diagnostics;

/// <summary>
/// The namespace contains the controller classes which are important for routing to the specific page on changing the URL and the main functionality lies in this controller classes whcih inherits Controller class.
/// </summary>
namespace UseCaseInjector.Controllers
{
    /// <summary>
    /// Main Home controller which has all the action methods of the Home page which are MQ and REST. It also has the values of dropdown list to be populated in both the pages
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Object of MQModel class which is initialized in the MQ action method
        /// </summary>
        MQModel mqobject = null;
        /// <summary>
        /// Object of RESTModel class which is initialized in the REST action method
        /// </summary>
        RESTModel restobject = null;
        /// <summary>
        /// Action method which gives the output as MQ view. It validates the system user on opening the page and if the validation is success it redirects the page accordingly.
        /// </summary>
        /// <returns>MQ View</returns>
        public ActionResult MQ()
        {
            mqobject = new MQModel();
            ViewBag.Username = System.Web.HttpContext.Current.User.Identity.Name.ToString()!=""? System.Web.HttpContext.Current.User.Identity.Name.Split('\\')[1]:"";
            var user = System.Web.HttpContext.Current.User.Identity.Name.ToString();
            UserLoginViewModel loginDetails = null;
            if (ValidateUser(user,out loginDetails) && (loginDetails.Role.Equals("PowerUser")|| loginDetails.Role.Equals("User")))
            {
                mqobject.userlogindetails = loginDetails;
                return View(mqobject);
            }
            else
            {
                dbSecurityEntities db = new dbSecurityEntities();
                dbAccess accessdetails = db.dbAccesses.SingleOrDefault(x => x.UserID == 1);
                return Content("<h2>Access is Denied for the user:"+user+ "</h2><br /><h4>For Access related issues - Please Contact <a href = 'mailto: vdc_int_test_mbrdi@daimler.com'>vdc_int_test_mbrdi@daimler.com</a></h4>");
            }
        }

        /*Make this as default URL*/
        /// <summary>
        /// Index action method which is the default page and is configured to redirect to REST view
        /// </summary>
        /// <returns>REST view</returns>
        public ActionResult Index()
        {
            return RedirectToAction("REST", "Home");
        }
        /// <summary>
        /// Action method for REST and returns the REST view. Firstly, Validates the user if it is valid user or not and then returns the view accordingly.
        /// </summary>
        /// <returns>REST view</returns>
        public ActionResult REST()
        {
            restobject = new RESTModel();
            ViewBag.Username = System.Web.HttpContext.Current.User.Identity.Name != "" ? System.Web.HttpContext.Current.User.Identity.Name.Split('\\')[1] : "";
            var user = System.Web.HttpContext.Current.User.Identity.Name.ToString();
            UserLoginViewModel loginDetails = null;
            if (ValidateUser(user, out loginDetails) && (loginDetails.Role.Equals("PowerUser") || loginDetails.Role.Equals("User")))
            {
                restobject.userlogindetails = loginDetails;
                return View(restobject);
            }
            else
            {
                return Content("<h2>Access is Denied for the user:" + user + "</h2><br /><h4>For Access related issues - Please Contact <a href = 'mailto: vdc_int_test_mbrdi@daimler.com'>vdc_int_test_mbrdi@daimler.com</a></h4>");
            }
        }
        /// <summary>
        /// Returns the Json values of the dropdown of Market in MQ view. The values of other dropdowns are loaded baased on the user selection of market region
        /// </summary>
        /// <param name="value">selected value in market dropdown of MQ view</param>
        /// <returns>Json with the dropdown values of source and application dropdowns</returns>
        [HttpGet]
        public JsonResult GetDropdownList(string value)
        {
            List<string> sourcedata = new List<string>();
            List<string> appdata = new List<string>();

            if (value == "CHN"|| value == "USA" || value == "AMAP")
            {
                sourcedata.Add("HERMES");
                appdata.Add("Accident Management");
                appdata.Add("Breakdown Management");
                appdata.Add("Maintenance Management");
                appdata.Add("Predictive");
                appdata.Add("Unknown");
                return Json(new { source_data = sourcedata, app_data=appdata }, JsonRequestBehavior.AllowGet);
            }
            else if(value == "ECE")
            {
                sourcedata.Add("KOM");
                sourcedata.Add("HERMES");
                sourcedata.Add("VehicleAdapter");
                sourcedata.Add("TRUCK");
                return Json(new { source_data = sourcedata, app_data = "" }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { source_data = "", app_data =""}, JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// Determines The values to be populated in the dropdown of Environment in MQ and REST view
        /// </summary>
        /// <returns>Json output with the values to be populated in environment dropdown</returns>
        [HttpGet]
        public JsonResult GetDropdownList_Env()
        {
            List<string> EnvData = new List<string>();
            EnvData.Add("INT");
            EnvData.Add("INT2");
            EnvData.Add("PROD");
            return Json(new { Env_Data = EnvData }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Creates the dropdown list values of market field in the MQ page.
        /// </summary>
        /// <returns>json result with the drop down values of market field</returns>
        [HttpGet]
        public JsonResult GetDropdownList_Market()
        {
            List<string> MarketData = new List<string>();
            MarketData.Add("CHN");
            MarketData.Add("ECE");
            MarketData.Add("USA");
            MarketData.Add("AMAP");
            return Json(new { Market_Data = MarketData }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Creates the dropdown list values of producer field in the REST page.
        /// </summary>
        /// <returns>json result with the dropdown values of producer field</returns>
        [HttpGet]
        public JsonResult GetDropdownList_Producer()
        {
            List<string> ProducerData = new List<string>();
            ProducerData.Add("DaiVB/Mmc");
            ProducerData.Add("VehicleAdapter");
            ProducerData.Add("TD USA");
            ProducerData.Add("TRUCKS");
            ProducerData.Add("Xentry Diagnosis");
            return Json(new { Producer_Data = ProducerData }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Drop down values for Application dropdown in MQ page.
        /// </summary>
        /// <param name="value">value selected in the dropdown</param>
        /// <returns>the dropdown values to be populated for based on application value</returns>
        [HttpGet]
        public JsonResult GetDropdownList_App(string value)
        {
            List<string> appdata = new List<string>();
            if (value == "KOM")
            {
                appdata.Add("Accident Management");
                appdata.Add("Breakdown Management");
                appdata.Add("Maintenance Management");
                appdata.Add("Predictive");
                appdata.Add("Shorttest");
                return Json(new { app_data = appdata }, JsonRequestBehavior.AllowGet);
            }
            else if (value == "HERMES")
            {
                appdata.Add("Accident Management");
                appdata.Add("Breakdown Management");
                appdata.Add("Maintenance Management");
                appdata.Add("Predictive");
                appdata.Add("Unknown");
                return Json(new { app_data = appdata }, JsonRequestBehavior.AllowGet);
            }
            else if (value == "VehicleAdapter")
            {
                appdata.Add("Accident Management");
                appdata.Add("Breakdown Management");
                appdata.Add("Maintenance Management");
                return Json(new { app_data = appdata }, JsonRequestBehavior.AllowGet);
            }
            else if (value == "TRUCK")
            {
                appdata.Add("shorttest");
                return Json(new { app_data = appdata }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { app_data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// populates the dropdown list of region and usecase fields based on the selection of the value in dropdown list of producer in REST page
        /// </summary>
        /// <param name="value">selected value in the dropdown of producer field</param>
        /// <returns>Json result with the dropdown values populated in the region and usecase fields</returns>
        [HttpGet]
        public JsonResult GetDropdownList_Rest(string value)
        {
            List<string> regiondata = new List<string>();
            List<string> usecasedata = new List<string>();

            if (value == "DaiVB/Mmc")
            {
                regiondata.Add("AMAP");
                regiondata.Add("CN");
                regiondata.Add("ECE");
                return Json(new { region_data = regiondata, usecase_data="" }, JsonRequestBehavior.AllowGet);
            }
            else if (value == "VehicleAdapter")
            {
                usecasedata.Add("Maintenance");
                usecasedata.Add("Breakdown");
                return Json(new { region_data = "",usecase_data = usecasedata }, JsonRequestBehavior.AllowGet);
            }
            else if(value == "TD USA")
            {
                usecasedata.Add("STR");
                usecasedata.Add("QTR");
                usecasedata.Add("WEM");
                usecasedata.Add("ESR");
                return Json(new { region_data = "", usecase_data = usecasedata }, JsonRequestBehavior.AllowGet);
            }
            else if(value == "TRUCKS")
            {
                usecasedata.Add("QTR");
                return Json(new { region_data = "", usecase_data = usecasedata }, JsonRequestBehavior.AllowGet);
            }
            else if(value == "Xentry Diagnosis")
            {
                usecasedata.Add("AKT");
                usecasedata.Add("RQT");
                usecasedata.Add("Async RQT");
                return Json(new { region_data = "", usecase_data = usecasedata }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { region_data = "", usecase_data = "" }, JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// Dropdown list populated in the usecase dropdown field if the value of ECE is selected in the producer field of REST view.
        /// </summary>
        /// <param name="value">selected value in the dropdown list of producer field</param>
        /// <returns>Json result to be populated in the usecase dropdown</returns>
        [HttpGet]
        public JsonResult GetDropdownList_ECE(string value)
        {
            List<string> usecasedata = new List<string>();
            if(value == "ECE")
            {
                usecasedata.Add("STR");
                usecasedata.Add("VSR");
                usecasedata.Add("QTR");
                usecasedata.Add("WEM");
                usecasedata.Add("ESR");
                usecasedata.Add("EDD");
                return Json(new {  usecase_data = usecasedata }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { usecase_data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Post method where the file upload is performed in the MQ page. This action method is called when the upload button in the MQ view is clicked and it stores the given file in the server uploads folder. 
        /// Once the file is stored the data is read in the file is read and posted to the text area present below th upload field.
        /// Throws error if the file is empty or invalid or does not exist in the result field
        /// </summary>
        /// <returns>content of the file or error message</returns>
        [HttpPost]
        public string Upload()
        {
            string fileName = Request.Headers["X-File-Name"];
            string fileType = Request.Headers["X-File-Type"];
            int fileSize = Convert.ToInt32(Request.Headers["X-File-Size"]);
            //File's content is available in Request.InputStream property
            System.IO.Stream fileContent = Request.InputStream;
            //Creating a FileStream to save file's content
            System.IO.FileStream fileStream = System.IO.File.Create(Server.MapPath("~/App_Data/Uploads") + fileName);
            fileContent.Seek(0, System.IO.SeekOrigin.Begin);
            //Copying file's content to FileStream
            fileContent.CopyTo(fileStream);
            fileStream.Dispose();
            if (System.IO.File.Exists(Server.MapPath(@"~/App_Data/Uploads") + fileName))
            {
                string data = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/Uploads") + fileName);
                if (data == null || data == String.Empty)
                {
                    return "File is empty";
                }
                else
                {
                    return data;
                }
            }
            else
            {
                return "File is not uploaded.";
            }
        }
        /// <summary>
        /// This method validates the user login and is called both by the MQ and REST page on loading the page.
        /// The model userloginviewmodel which reads the data from database gives the database values to validate the loggedin user.
        /// </summary>
        /// <param name="Username">User logged in to the page</param>
        /// <param name="logindetails">out parameter gives the details of loggedin user in database</param>
        /// <returns>if the user exists or not</returns>
        public Boolean ValidateUser(string Username,out UserLoginViewModel logindetails)
        {
            logindetails = null;
            try
            {

                dbSecurityEntities db = new dbSecurityEntities();
                dbAccess accessdetails = db.dbAccesses.SingleOrDefault(x => x.UserName.Contains(Username));
                if (accessdetails != null)
                {
                    logindetails = new UserLoginViewModel();
                    logindetails.UserID = accessdetails.UserID;
                    logindetails.UserName = accessdetails.UserName;
                    logindetails.Role = accessdetails.Role;
                    return true;
                }

            }
            catch(Exception e)
            {
               // EventLog.WriteEntry("UseCaseInjector", "Exception occured while extracting the User Data : " + e.Message, EventLogEntryType.Error);
            }
            return false;
        }
        /// <summary>
        /// Post request which performs the operatons after submit and produces the result in result field of MQ view by taking the inputs from MQModel object
        /// </summary>
        /// <param name="m">object of MQmodel page</param>
        /// <returns>Json result with all the exceptions during the injection</returns>
        [HttpPost, ValidateInput(false)]
        public JsonResult ResultView_MQ(MQModel m)
        {
            string Environment = m.Environment;
            string Market = m.Market;
            string Source = m.Source;
            string Application = m.Application;
            string data = m.inputfileData;
            string syntax = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/xmlData.txt"));
            Injection_MQ inject = new Injection_MQ();
            string[] result = inject.triggerInjection(Environment, Market, Source, Application, data,syntax);
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Post request which performs the operatons after submit and produces the result in result field of REST view by taking the inputs from RESTModel object
        /// </summary>
        /// <param name="m">RESTModel object</param>
        /// <returns>Exceptions occured during injection, Tracking ID, Endpoint</returns>
        [HttpPost, ValidateInput(false)]
        public JsonResult ResultView_REST(RESTModel m)
        {
            string Environment = m.Environment;
            string Producer = m.Producer;
            string Region = m.Region;
            string Usecase = m.Usecase;
            string VIN = m.VIN;
            string TrackingID = m.TrackingID;
            string data = m.InputData;
            Injection_REST inject = new Injection_REST();
            string[] result = inject.triggerInjection(Environment, Producer, Region, Usecase, VIN, TrackingID, data);
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
    }
}