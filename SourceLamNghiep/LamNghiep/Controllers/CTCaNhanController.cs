using LamNghiep.BUL.Bussiness;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    public class CTCaNhanController : Controller
    {
        //
        // GET: /CTCaNhan/
        public ActionResult Index()
        {
            KetHoachCTBussinessService ketHoachCTBussinessService = new KetHoachCTBussinessService();
            string userName = "admin";
            List<KeHoachCT> listKeHoachCT = ketHoachCTBussinessService.GetKeHoachCaNhan(userName);
            return View(listKeHoachCT);
        }

        //
        // GET: /CTCaNhan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CTCaNhan/Create
        public ActionResult Create()
        {
            return View();
        }
        public JsonResult GetJson(JQueryDataTableParamModel param)
        {
            // check login

            int sortColumnIndex = param.ISortCol_0;
            string order = param.SSortDir_0;

            string orderBy = string.Empty;

            switch (sortColumnIndex)
            {
                case 0:
                    orderBy = "Name";
                    break;
                case 1:
                    orderBy = "Description";
                    break;
                case 2:
                    orderBy = "Code";
                    break;
                case 3:
                    orderBy = "CreateDate";
                    break;
            }

            KetHoachCTBussinessService ketHoachCTBussinessService = new KetHoachCTBussinessService();
            string userName = "admin";
            List<KeHoachCT> listKeHoachCT = ketHoachCTBussinessService.GetKeHoachCaNhan(userName);

            foreach (var item in listKeHoachCT)
            {
                item.GioView = item.Gio.ToString();
                item.TuNgay = item.TuNgay + " - " + item.DenNgay;
                if (item.MucDo==1)
                {
                    item.MucDoView = "rất quan trọng";
                }
                if (item.MucDo == 2)
                {
                    item.MucDoView = "quan trọng";
                }
                else
                {
                    item.MucDoView = "bình thường";
                }
            }

            int totalRecords = 0;

            totalRecords = 1;

            // return jon datatable
            return Json(new
            {
                sEcho = param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = listKeHoachCT
            }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /CTCaNhan/Create
        [HttpPost]
        public ActionResult Create(KeHoachCT keHoachCT)
        {
            try
            {
                KetHoachCTBussinessService ketHoachCTBussinessService = new KetHoachCTBussinessService();
                keHoachCT.UserName="admin";
                ketHoachCTBussinessService.AddKeHoachCT(keHoachCT);
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CTCaNhan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /CTCaNhan/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CTCaNhan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CTCaNhan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
