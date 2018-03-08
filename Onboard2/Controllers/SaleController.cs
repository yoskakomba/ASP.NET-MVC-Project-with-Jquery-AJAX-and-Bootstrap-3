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

        // ADD OR EDIT CHECKING ID IF 0 OPEN PARTIAL VIEW CREATE MODAL
        public ActionResult AddEditProductSold(int Id)
        {
            var db = new Onboard2DbContext();
            var model = new MySale();
            if (Id > 0)
            {
                var prodsold = db.ProductSolds.SingleOrDefault(x => x.Id == Id);
                var customer = db.Customers.SingleOrDefault(c => c.Id == prodsold.CustomerId);
                var store = db.Stores.SingleOrDefault(s => s.Id == prodsold.StoreId);
                var product = db.Products.SingleOrDefault(p => p.Id == prodsold.ProductId);

                model.Id = prodsold.Id;
                model.ProductId = product.Id;
                model.ProductName = product.Name;
                model.CustomerId = customer.Id;
                model.CustomerName = customer.Name;
                model.StoreId = store.Id;
                model.StoreName = store.Name;
                model.DateSold = prodsold.DateSold;
            }
            return PartialView("AddOrEdit", model);
        }

        // GET POST ADD OR EDIT
        [HttpPost]
        public ActionResult UpdateOrCreateProductSold(ProductSold model)
        {
           
            var db = new Onboard2DbContext();
            if (model.Id > 0)
            {
                // UPDATE
                var prodsold = db.ProductSolds.SingleOrDefault(x => x.Id == model.Id);

                prodsold.ProductId = model.ProductId;
                prodsold.CustomerId = model.CustomerId;
                prodsold.StoreId = model.StoreId;
                prodsold.DateSold = model.DateSold;
                db.SaveChanges();
            }

            else
            {
                // INSERT
                ProductSold prodsold = new ProductSold();
                prodsold.ProductId = model.ProductId;
                prodsold.CustomerId = model.CustomerId;
                prodsold.StoreId = model.StoreId;
                prodsold.DateSold = model.DateSold;

                db.ProductSolds.Add(prodsold);
                db.SaveChanges();
                    
            }
            return Json(model);
        }
            
        // LOAD CONTROLLER FOR DROPDOWN SELECT
        public ActionResult GetCustomerList()
        {
            using (var db = new Onboard2DbContext())
            {
                var allCust = db.Customers.Select(x => new MyCustomer
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                }).ToList();
                return Json(allCust, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProductList()
        {
            using (var db = new Onboard2DbContext())
            {
                var allProd = db.Products.Select(x => new MyProduk
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
                return Json(allProd, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetStoreList()
        {
            using (var db = new Onboard2DbContext())
            {
                var allStor = db.Stores.Select(x => new MyStore
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
                return Json(allStor, JsonRequestBehavior.AllowGet);
            }
        }
    }

    
}
