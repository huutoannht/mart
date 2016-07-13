﻿using LamNghiep.BUL.Bussiness;
using LamNghiep.DAL.DataServices;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
        {
            LoginBussinessService loginBussinessService = new LoginBussinessService();
            if (loginBussinessService.CheckLogin(user))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
        //
        // GET: /Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Login/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Login/Create
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
        // GET: /Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Login/Edit/5
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
        // GET: /Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Login/Delete/5
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
