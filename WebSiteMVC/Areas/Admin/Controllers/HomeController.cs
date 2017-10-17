using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteMVC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.Dashboard = true;
        }

        // GET: Admin/Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}