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
    public class PositionSettingController : CategoryBaseController
    {
        public PositionSettingController()
            : base(BePage.PositionSetting)
        {
            CategoryType = CategoryType.Position;
            AddNewTitle = CategoryRes.AddNewPosition;
            EditTitle = CategoryRes.EditPosition;
        }

        [View]
        public ActionResult Index(CategoryIndexModel model)
        {
            PopulateIndexModel(model);

            return View(model);
        }
    }
}