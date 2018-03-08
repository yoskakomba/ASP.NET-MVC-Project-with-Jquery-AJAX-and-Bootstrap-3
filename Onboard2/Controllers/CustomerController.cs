using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Onboard2.Models;

namespace Onboard2.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer List
        public ActionResult GetCustomerList()
        {
            using (var db = new Onboard2DbContext())
            {
                var cust = db.Customers.Select(x => new MyCustomer
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                }).ToList();
                return View(cust);
            }
        }

        // SHOW DETAILS
        public ActionResult ShowCustomer(int Id)
        {
            Onboard2DbContext db = new Onboard2DbContext();
            List<MyCustomer> listCust = db.Customers.Where(x => x.Id == Id).Select(x => new MyCustomer()
            {
                Name = x.Name,
                Address = x.Address

            }).ToList();

            ViewBag.CustomerList = listCust;
            return PartialView("ShowCustomer");
        }

        // DELETE
        public JsonResult DeleteCustomer(int Id)
        {
            using (var db = new Onboard2DbContext())
            {

                var cust = db.Customers.FirstOrDefault(x => x.Id == Id);
                var prodsold = db.ProductSolds.FirstOrDefault(x => x.Id == Id);
                var custid = db.ProductSolds.FirstOrDefault(x => x.CustomerId == Id);

                if (cust != null) db.Customers.Remove(cust);
                if (prodsold != null) db.ProductSolds.Remove(prodsold);
                if (custid != null) db.ProductSolds.Remove(custid);
                db.SaveChanges();
                return Json(JsonRequestBehavior.AllowGet);
            }
        }

        // ADD OR EDIT
        public ActionResult AddOrEdit(int Id)
        {
            Onboard2DbContext db = new Onboard2DbContext();
            MyCustomer model = new MyCustomer();
            if (Id > 0)
            {
                Customer cust = db.Customers.SingleOrDefault(x => x.Id == Id);
                model.Id = cust.Id;
                model.Name = cust.Name;
                model.Address = cust.Address;
            }
            return PartialView("AddOrEdit", model);
        }

        // UPDATE OR CREATE NEW
        [HttpPost]
        public ActionResult UpdateOrCreateCustomer(MyCustomer model)
        {
            try
            {
                Onboard2DbContext db = new Onboard2DbContext();
                if (model.Id > 0)
                {
                    //update
                    Customer cust = db.Customers.FirstOrDefault(x => x.Id == model.Id);
                    if (cust != null)
                    {
                        cust.Id = model.Id;
                        cust.Name = model.Name;
                        cust.Address = model.Address;
                    }
                    db.SaveChanges();
                }
                else
                {
                    //Insert
                    Customer cust = new Customer();
                    cust.Name = model.Name;
                    cust.Address = model.Address;
                    db.Customers.Add(cust);
                    db.SaveChanges();
                }
                return RedirectToAction("GetCustomerList");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}