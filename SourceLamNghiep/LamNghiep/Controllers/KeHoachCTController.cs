﻿using LamNghiep.BUL.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    public class KeHoachCTController : Controller
    {
        public ActionResult Index()
        {
            KetHoachCTBussinessService ketHoachCTBussinessService = new KetHoachCTBussinessService();
            return View(ketHoachCTBussinessService.GetCurWeekKHCT());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}