﻿using System.Web.Mvc;
using log4net;

namespace MeetUp.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            ViewBag.Menu = "Events";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
