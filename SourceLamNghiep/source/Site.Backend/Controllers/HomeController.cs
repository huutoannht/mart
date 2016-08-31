using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.Backend.Library.UI;
using Data.DataContract;
using Share.Helper;

namespace Site.Backend.Controllers
{
    public class HomeController : AuthorisedController
    {
        public HomeController() : base(BePage.Home) { }

        public ActionResult Index()
        {
            //SiteUtils.GetVietNamProvines();
            //SiteUtils.GetVietNamDistricts();
            return View();
        }
    }
}