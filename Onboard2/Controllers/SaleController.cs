using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Onboard2.Models;

namespace Onboard2.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult GetSaleList()
        {
            using (var db = new Onboard2DbContext())
            {
                var saleList = db.ProductSolds.Select(x => new MySale
                {
                    Id = x.Id,
                    ProductName = x.Product.Name,
                    CustomerName = x.Customer.Name,
                    StoreName = x.Store.Name,
                    DateSold = x.DateSold
                }).ToList();

                return View(saleList);
            }
        }
        
        // SHOW DETAILS
        public ActionResult ShowSale(int Id)
        {
            Onboard2DbContext db = new Onboard2DbContext();

            List<MySale> listSales = db.ProductSolds.Where(x => x.Id == Id).Select(x => new MySale()
            {
                Id = x.Id,
                ProductName = x.Product.Name,
                CustomerName = x.Customer.Name,
                StoreName = x.Store.Name,
                DateSold = x.DateSold

            }).ToList();

            ViewBag.SaleList = listSales;
            return PartialView("ShowSale");
        }

        // DELETE
        public JsonResult DeleteSale(int Id)
        {
            using (var db = new Onboard2DbContext())
            {
                var prodsold = db.ProductSolds.FirstOrDefault(x => x.Id == Id);
                if (prodsold != null) db.ProductSolds.Remove(prodsold);
                db.SaveChanges();
                
                return Json(JsonRequestBehavior.AllowGet);
            }
        }

        // ADD OR EDIT
        public ActionResult AddEditProductSold(int Id)
        {
            var db = new Onboard2DbContext();
            
            List<Product> list = db.Products.ToList();
            ViewBag.ProductList = new SelectList(list, "Id", "Name");

            List<Customer> customerlist = db.Customers.ToList();
            ViewBag.CustomerList = new SelectList(customerlist, "Id", "Name");

            List<Store> storelist = db.Stores.ToList();
            ViewBag.StoreList = new SelectList(storelist, "Id", "Name");

            var model = new MySale();

            if(Id > 0)
            {
                var prodsold = db.ProductSolds.SingleOrDefault(x => x.Id == Id);
                model.Id = prodsold.Id;
                model.ProductName = prodsold.ProductId.ToString();
                model.CustomerName = prodsold.CustomerId.ToString();
                model.StoreName = prodsold.StoreId.ToString();
                model.DateSold = prodsold.DateSold;
            }

            return PartialView("Partial2", model);


        }

        // GET POST ADD OR EDIT
        [HttpPost]
        public ActionResult CreateProductSold(MySale model)
        {
            try
            {
                var db = new Onboard2DbContext();

                List<Product> list = db.Products.ToList();
                ViewBag.ProductList = new SelectList(list, "Id", "Name");

                List<Customer> customerlist = db.Customers.ToList();
                ViewBag.CustomerList = new SelectList(customerlist, "Id", "Name");

                List<Store> storelist = db.Stores.ToList();
                ViewBag.StoreList = new SelectList(storelist, "Id", "Name");

                if (model.Id > 0)
                {
                    // UPDATE
                    var prodsold = db.ProductSolds.SingleOrDefault(x => x.Id == model.Id);

                    prodsold.ProductId = Convert.ToInt32(model.ProductName);
                    prodsold.CustomerId = Convert.ToInt32(model.CustomerName);
                    prodsold.StoreId = Convert.ToInt32(model.StoreName);
                    prodsold.DateSold = model.DateSold;
                    db.SaveChanges();
                }

                else
                {
                    // INSERT
                    ProductSold prodsold = new ProductSold();
                    prodsold.ProductId = Convert.ToInt32(model.ProductName);
                    prodsold.CustomerId = Convert.ToInt32(model.CustomerName);
                    prodsold.StoreId = Convert.ToInt32(model.StoreName);
                    prodsold.DateSold = model.DateSold;

                    db.ProductSolds.Add(prodsold);
                    db.SaveChanges();

                }

                return RedirectToAction("GetSaleList");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
