using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteMVC.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.Dashboard = true;
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}