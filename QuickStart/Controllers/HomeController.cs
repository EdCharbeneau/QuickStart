using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickStart.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MontlySalesByEmployee()
        {
            return View();
        }

        public ActionResult QuarterToDateSales()
        {
            return View();
        }

    }
}