using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tour_Package_Manager.Controllers.website
{
    public class WebHomeController : Controller
    {
        // GET: WebHome
        public ActionResult Index()
        {
            return View();
        }
    }
}