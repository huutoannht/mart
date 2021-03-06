﻿using System;
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
    public class NumberOfStaffSettingController : CategoryBaseController
    {
        public NumberOfStaffSettingController()
            : base(BePage.NumberOfStaffSetting)
        {
            CategoryType = CategoryType.NumberOfStaff;
            AddNewTitle = CategoryRes.AddNewNumberOfStaff;
            EditTitle = CategoryRes.EditNumberOfStaff;
        }

        [View]
        public ActionResult Index(CategoryIndexModel model)
        {
            PopulateIndexModel(model);

            return View(model);
        }
    }
}