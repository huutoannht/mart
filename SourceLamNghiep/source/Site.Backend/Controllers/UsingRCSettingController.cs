using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DataContract;
using Models.Category;
using Share.Messages.BeScreens.CategoryRes;
using Site.Backend.Library.Attribute;

namespace Site.Backend.Controllers
{
    public class UsingRCSettingController : CategoryBaseController
    {
        public UsingRCSettingController()
            : base(BePage.UsingRCSetting)
        {
            CategoryType = CategoryType.UsingRC;
            AddNewTitle = CategoryRes.AddNewUsingRC;
            EditTitle = CategoryRes.EditUsingRC;
        }

        [View]
        public ActionResult Index(CategoryIndexModel model)
        {
            PopulateIndexModel(model);

            return View(model);
        }
    }
}