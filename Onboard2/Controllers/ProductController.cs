using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Onboard2.Models;

namespace Onboard2.Controllers
{
    public class ProductController : Controller
    {
        // PRODUCT LIST VIEW
        public ActionResult GetProductList()
        {
            using (var db = new Onboard2DbContext())
            {
                var product = db.Products.Select(x => new MyProduk
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).ToList();

                return View(product);
            }
        }
        
        // DELETE
        public JsonResult DeleteProduct(int Id)
        {
            using (var db = new Onboard2DbContext())
            {

                var prod = db.Products.FirstOrDefault(x => x.Id == Id);
                var prodsold = db.ProductSolds.FirstOrDefault(x => x.Id == Id);
                var prodid = db.ProductSolds.FirstOrDefault(x => x.ProductId == Id);

                if (prod != null) db.Products.Remove(prod);
                if (prodsold != null) db.ProductSolds.Remove(prodsold);
                if (prodid != null) db.ProductSolds.Remove(prodid);
                db.SaveChanges();

                return Json(JsonRequestBehavior.AllowGet);
            }
        }

        // SHOW DETAILS
        public ActionResult ShowProduct(int Id)
        {
            Onboard2DbContext db = new Onboard2DbContext();
            List<MyProduk> listProd = db.Products.Where(x => x.Id == Id).Select(x => new MyProduk()
            {
                Name = x.Name,
                Price = x.Price

            }).ToList();

            ViewBag.ProductList = listProd;
            return PartialView("ShowProduct");
        }

        // ADD OR EDIT
        public ActionResult AddOrEdit(int Id)
        {
            Onboard2DbContext db = new Onboard2DbContext();
            MyProduk model = new MyProduk();
            if (Id > 0)
            {
                Product prod = db.Products.SingleOrDefault(x => x.Id == Id);
                model.Id = prod.Id;
                model.Name = prod.Name;
                model.Price = prod.Price;
            }
            
            return PartialView("AddOrEdit", model);
        }

        // UPDATE OR CREATE NEW
        [HttpPost]
        public ActionResult UpdateOrCreateProduct(MyProduk model)
        {
            try
            {
                Onboard2DbContext db = new Onboard2DbContext();
                if (model.Id > 0)
                {
                    //update
                    Product prod = db.Products.SingleOrDefault(x => x.Id == model.Id);
                    prod.Id = model.Id;
                    prod.Name = model.Name;
                    prod.Price = model.Price;
                    db.SaveChanges();
                }
                else
                {
                    //Insert
                    Product prod = new Product();
                    prod.Name = model.Name;
                    prod.Price = model.Price;
                    db.Products.Add(prod);
                    db.SaveChanges();
                }

                return RedirectToAction("GetProductList");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }


}