using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LamNghiep.BUL.Bussiness;

namespace LamNghiep.Controllers
{
    public class PhongHopController : Controller
    {
        //
        // GET: /DatPhong/
        public ActionResult Index()
        {
            PhongBussinessService phongBussinessService = new PhongBussinessService();
            return View(phongBussinessService.GetPhong());
        }

        //
        // GET: /DatPhong/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DatPhong/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DatPhong/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DatPhong/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DatPhong/Edit/5
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
        // GET: /DatPhong/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DatPhong/Delete/5
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
