using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DataContract;
using Models.Customer;
using Share.Messages.BeScreens.CustomerRes;
using Site.Backend.Library.Attribute;

namespace Site.Backend.Controllers
{
    public class PotentialCustomerController : CustomerBaseController
    {
        public PotentialCustomerController()
            : base(BePage.PotentialCustomer, CustomerType.Potential)
        {
            AddNewTitle = CustomerRes.AddNewPotentialCustomer;
            EditTitle = CustomerRes.EditPotentialCustomer;
        }

        [View]
        public ActionResult Index(CustomerIndexModel model)
        {
            PopulateIndexModel(model);
            return View(model);
        }
    }
}