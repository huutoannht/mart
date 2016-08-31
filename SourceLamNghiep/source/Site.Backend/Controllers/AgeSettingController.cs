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
    public class AgeSettingController : CategoryBaseController
    {
        public AgeSettingController()
            : base(BePage.AgeSetting)
        {
            CategoryType = CategoryType.Age;
            AddNewTitle = CategoryRes.AddNewAge;
            EditTitle = CategoryRes.EditAge;
        }

        [View]
        public ActionResult Index(CategoryIndexModel model)
        {
            PopulateIndexModel(model);

            return View(model);
        }
    }
}