﻿using ElectronicShop.App_Start;
using ElectronicShop.Filters;
using ElectronicShopBL.IBL;
using ElectronicShopBL.ViewModels;
using ElectronicShopRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace ElectronicShop.Controllers
{
    public class CustomerController : Controller
    {
        IBussinseContext bussinseContext = UnityConfig.Container.Resolve<IBussinseContext>();
     
        // GET: CustomerController
        [AuthorizeAdmin]
        public ActionResult Index()
        {
            var vm = CustomerVM.getCustomers(bussinseContext);
            return View(vm);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        [AuthorizeAdmin]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult Create(CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                var user = CustomerVM.addCustomer(bussinseContext, customerVM);
                if (user == null)
                {
                    ViewBag.LoginError = true;
                    ViewBag.Message = "UserName or password is incorrect";
                  
                }
                if (user != null)
                {
                    HttpContext.Session.SetInt32("CurrentUserId", user.id);
                    HttpContext.Session.SetString("UserName", user.userName);
                    HttpContext.Session.SetInt32("UserRole", user.role.Value);
                }

           
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(customerVM);

        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
