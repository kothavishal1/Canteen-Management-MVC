using AutoMapper;
using CMApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMAppDataLayer;
namespace CMApp.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(){
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Customer, CMAppDataLayer.Customer>();
            });
        }

        // GET: Admin/Customer
        public ActionResult Index()
        {
            var customers = _ctx.Customers.ToList();
            var model = Mapper.Map<IEnumerable<Customer>, IEnumerable<CMAppDataLayer.Customer>>(customers);
            return View("Index", model);
        }

        // GET: Admin/Customer/Details/5
        public ActionResult Details(int id)
        {
            var customer = _ctx.Customers.FirstOrDefault(c => c.CID == id);
            var model = Mapper.Map<Customer, CMAppDataLayer.Customer>(customer);
            return View(model);
        }

        // GET: Admin/Customer/Delete/5
        public ActionResult Delete(int id)
        {
            var customer = _ctx.Customers.FirstOrDefault(c => c.CID == id);
            _ctx.Customers.Remove(customer);
            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
