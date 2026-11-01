 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebAppStartUp.Models;

namespace WebAppStartUp.Common
{
    public class Utility
    {
        public static Model_User GetLoggedInUserDetails()
        {
            var userDetails = new Model_User();
            try
            {                
                FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
                userDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_User>(identity.Ticket.UserData);
                
            }
            catch (Exception ex )
            {

               
            }
            
            return userDetails;

        }


    }
}