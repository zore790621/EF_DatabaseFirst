﻿using EF_Database_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EF_Database_MVC.Controllers
{
    public class ProductViewController : Controller
    {
        private DataModel db = new DataModel();
        // GET: ProductView
        public ActionResult Index()
        {
            var prv = from p in db.Products
                      select new ProductView
                      {
                          Id = p.ProductID,
                          Name = p.ProductName,
                          Price = (decimal)p.UnitPrice,
                          Tax = (decimal)p.UnitPrice * (decimal)1.05,
                          UnitsInStock = (int)p.UnitsInStock
                      };
            return View(prv);
        }
        public ActionResult ProductEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ProductView prv = new ProductView
            {
                Id = product.ProductID,
                Name = product.ProductName,
                Price = (decimal)product.UnitPrice,
                UnitsInStock = (int)product.UnitsInStock
            };
            return View(prv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit([Bind(Include = "Id,Name,Price,UnitsInStock")] ProductView p)
        {
            if (ModelState.IsValid)
            {
                Product product = db.Products.Find(p.Id);
                product.ProductName = p.Name;
                product.UnitPrice = p.Price;
                product.UnitsInStock = (short?)p.UnitsInStock;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }
    }
}