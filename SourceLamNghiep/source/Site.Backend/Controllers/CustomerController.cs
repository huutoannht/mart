using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DataContract.CustomerDC;
using ML.Common;
using Data.DataContract;
using Models.Customer;
using Share.Helper;
using Share.Helper.Extension;
using Share.Messages;
using Share.Messages.BeScreens.CustomerRes;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;

namespace Site.Backend.Controllers
{
    public class CustomerController : CustomerBaseController
    {
        public CustomerController() : base(BePage.Customer, CustomerType.Current)
        {
            AddNewTitle = CustomerRes.AddNewCustomer;
            EditTitle = CustomerRes.EditCustomer;
        }

        [View]
        public ActionResult Index(CustomerIndexModel model)
        {
            PopulateIndexModel(model);
            return View(model);
        }
    }
}