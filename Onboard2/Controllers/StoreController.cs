using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Onboard2.Models;

namespace Onboard2.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            using (var db = new Onboard2DbContext())
            {
                var stor = db.Stores.Select(x => new MyStore
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                }).ToList();

                return View(stor);
            }
        }


        // UPDATE OR CREATE NEW
        [HttpPost]
        public ActionResult Index(MyStore model)
        {
            try
            {
                Onboard2DbContext db = new Onboard2DbContext();

                if (model.Id > 0)
                {
                    //update
                    Store stor = db.Stores.FirstOrDefault(x => x.Id == model.Id);


                    if (stor != null)
                    {
                        stor.Id = model.Id;
                        stor.Name = model.Name;
                        stor.Address = model.Address;
                    }


                    db.SaveChanges();


                }
                else
                {
                    //Insert
                    Store stor = new Store();
                    stor.Name = model.Name;
                    stor.Address = model.Address;

                    db.Stores.Add(stor);
                    db.SaveChanges();

                }

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        // SHOW DETAILS
        public ActionResult ShowStore(int Id)
        {
            Onboard2DbContext db = new Onboard2DbContext();

            List<MyStore> listStor = db.Stores.Where(x => x.Id == Id).Select(x => new MyStore()
            {
                Name = x.Name,
                Address = x.Address

            }).ToList();

            ViewBag.StoreList = listStor;

            return PartialView("ShowStore");


        }

        // ADD OR EDIT
        public ActionResult AddOrEdit(int Id)
        {
            Onboard2DbContext db = new Onboard2DbContext();
            MyStore model = new MyStore();

            if (Id > 0)
            {
                Store stor = db.Stores.SingleOrDefault(x => x.Id == Id);
                model.Id = stor.Id;
                model.Name = stor.Name;
                model.Address = stor.Address;

            }

            return PartialView("AddOrEdit", model);


        }

        // DELETE
        public JsonResult DeleteStore(int Id)
        {
            using (var db = new Onboard2DbContext())
            {

                var stor = db.Stores.FirstOrDefault(x => x.Id == Id);
                var prodsold = db.ProductSolds.FirstOrDefault(x => x.Id == Id);
                var storid = db.ProductSolds.FirstOrDefault(x => x.StoreId == Id);

                if (stor != null) db.Stores.Remove(stor);
                if (prodsold != null) db.ProductSolds.Remove(prodsold);
                if (storid != null) db.ProductSolds.Remove(storid);
                db.SaveChanges();


                return Json(JsonRequestBehavior.AllowGet);
            }
        }
    }
}