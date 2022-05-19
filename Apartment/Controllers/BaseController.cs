using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apartment.Controllers
{
    public abstract class BaseController : Controller
    {
        string _name;
        string _isAdmin;


        public BaseController()
        {

        }

         public string userName
        {
            get
            {
                return _name;
            }
        }

        public string isAdmin
        {
            get
            {
                return _isAdmin;
            }
        }

        protected override void OnActionExecuting (ActionExecutingContext context)
        {
            //Get name          
            _name = User.Identity.Name;
            _isAdmin = User.IsInRole("Admin").ToString();
        }


    }
}