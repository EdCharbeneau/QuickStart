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

        public ActionResult Index()
        {
            return View();
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

        public ActionResult EmployeeAverageSales(
            int employeeId, 
            DateTime statsFrom, 
            DateTime statsTo)
        {
            var result = (from allSales in
                              (from o in db.Orders
                               join od in db.Order_Details on o.OrderID equals od.OrderID
                               where o.EmployeeID == employeeId && o.OrderDate >= statsFrom && o.OrderDate <= statsTo
                               select new
                               {
                                   EmployeeID = o.EmployeeID,
                                   Date = o.OrderDate,
                                   Sales = od.Quantity * od.UnitPrice
                               }
                                  ).ToList()
                          group allSales by new DateTime(allSales.Date.Value.Year, allSales.Date.Value.Month, 1) into g
                          select new MonthlySalesByEmployeeResult
                          {
                              EmployeeID = g.FirstOrDefault().EmployeeID,
                              EmployeeSales = g.Sum(x => x.Sales),
                              Date = g.Key,
                          }
            );

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeQuarterSales(int employeeId, DateTime statsTo)
        {
            DateTime startDate = statsTo.AddMonths(-3);
            var sales = db.Orders.Where(w => w.EmployeeID == employeeId)
                .Join(db.Order_Details, orders => orders.OrderID, orderDetails => orderDetails.OrderID, (orders, orderDetails) => new { Order = orders, OrderDetails = orderDetails })
                .Where(d => d.Order.OrderDate >= startDate && d.Order.OrderDate <= statsTo).ToList()
                .Select(o => new QuarterToDateSalesViewModel
                {
                    Current = (o.OrderDetails.Quantity * o.OrderDetails.UnitPrice) - (o.OrderDetails.Quantity * o.OrderDetails.UnitPrice * (decimal)o.OrderDetails.Discount)
                });
            //TODO: Generate the target based on team's average sales
            var result = new List<QuarterToDateSalesViewModel>() {
                     new QuarterToDateSalesViewModel {Current = sales.Sum(s=>s.Current), Target = 15000, OrderDate = statsTo}
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
