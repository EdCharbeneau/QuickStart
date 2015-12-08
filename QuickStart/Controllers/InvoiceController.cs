﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using QuickStart.Models;

namespace QuickStart.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly NorthwindDBContext db = new NorthwindDBContext();

        public ActionResult Index(InvoiceParameters model)
        {
            return View(model);
        }

        public ActionResult Invoices_Read([DataSourceRequest]DataSourceRequest request,
            String salesPerson,
            DateTime statsFrom, 
            DateTime statsTo)
        {
            var invoices = db.Invoices.Where(inv => inv.Salesperson == salesPerson)
                .Where(inv => inv.OrderDate >= statsFrom && inv.OrderDate <= statsTo);
            DataSourceResult result = invoices.ToDataSourceResult(request, invoice => new {
                OrderID = invoice.OrderID,
                CustomerName = invoice.CustomerName,
                OrderDate = invoice.OrderDate,
                ProductName = invoice.ProductName,
                UnitPrice = invoice.UnitPrice,
                Quantity = invoice.Quantity,
                Salesperson = invoice.Salesperson
            });

            return Json(result);
        }

        public ActionResult EmployeesList_Read([DataSourceRequest]DataSourceRequest request)
        {
            var employees = db.Employees.OrderBy(e => e.FirstName);

            return Json(employees.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
