using CMApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMAppDataLayer;
using CMAppBusiness.IServices;
using CMAppBusiness.Services;

namespace CMApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService _product;
        public HomeController()
        {
            _product = new ProductService();
        }
        
        public ActionResult Index()
        {
            List<Product> products = _ctx.Products.ToList<Product>();
            ViewBag.Products = products;
            return View();
        }

        public ActionResult Category(string catName)
        {
            List<Product> products = _product.GetCategory(catName);
            ViewBag.Products = products;
            return View("Index");
        }

        public ActionResult Suppliers()
        {
            List<Supplier> suppliers = _ctx.Suppliers.ToList<Supplier>();
            ViewBag.Suppliers = suppliers;
            return View();
        }

        public ActionResult Orders()
        {
            List<CMAppDataLayer.Models.Orders> ordersData = _product.getOrdersData();
            ViewBag.OrdersData = ordersData;
            return View();
        }

        public ActionResult AddToCart(int id)
        {
            addToCart(id);
            return RedirectToAction("Index");
        }

        private void addToCart(int pId)
        {
            // check if product is valid
            _product.addToCart(pId);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}