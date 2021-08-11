using CMApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMAppDataLayer;
namespace CMApp.Controllers
{
    public class HomeController : BaseController
    {
        
        public ActionResult Index()
        {

            List<Product> products = _ctx.Products.ToList<Product>();
            ViewBag.Products = products;
            return View();
        }

        public ActionResult Category(string catName)
        {
            List<Product> products;
            if (catName == "")
            {
                products = _ctx.Products.ToList<Product>();
            } else { 
                products = _ctx.Products.Where(p => p.Category == catName).ToList<Product>();
            }
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
            List<Product> Products = _ctx.Products.ToList<Product>();
            List<Customer> Customers = _ctx.Customers.ToList<Customer>();
            List<Order> Orders = _ctx.Orders.ToList<Order>();
            List<Order_Products> Order_Products = _ctx.Order_Products.ToList<Order_Products>();

            var ordersData = (from cust in Customers
                              join o in Orders on cust.CID equals o.CID
                              join op in Order_Products on o.OrderID equals op.OrderID
                              join p in Products on op.PID equals p.PID
                              select new CMApp.Models.Orders
                              {
                                  FName = cust.FName,
                                  LName = cust.LName,
                                  Phone = cust.Phone,
                                  OrderDate = o.OrderDate.ToString("dd/MM/yyyy"),
                                  DeliveryDate = o.DeliveryDate.ToString("dd/MM/yyyy"),
                                  PName =p.PName,
                                  Qty=op.Qty.ToString(),
                                  TotalSale=op.TotalSale.ToString(),
                                  Brand=p.Brand
                              }).ToList();
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
            Product product = _ctx.Products.FirstOrDefault(p => p.PID == pId);
            if (product != null && product.UnitsInStock > 0)
            {
                // check if product already existed
                ShoppingCartData cart = _ctx.ShoppingCartDatas.FirstOrDefault(c => c.PID == pId);
                if (cart != null)
                {
                    cart.Quantity++;
                }
                else
                {

                    cart = new ShoppingCartData
                    {
                        PName = product.PName,
                        PID = product.PID,
                        UnitPrice = product.UnitPrice,
                        Quantity = 1
                    };

                    _ctx.ShoppingCartDatas.Add(cart);
                }
                product.UnitsInStock--;
                _ctx.SaveChanges();
            }
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