using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DataContract;
using Site.Backend.Library.UI;

namespace Site.Backend.Controllers
{
    public class UnderConstructionController : AuthorisedController
    {
        public UnderConstructionController() : base(BePage.Home) { }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sales()
        {
            return View("Index");
        }

        public ActionResult Cash()
        {
            return View("Index");
        }
    }
}