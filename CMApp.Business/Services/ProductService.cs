using AutoMapper;
using CMAppBusiness.IServices;
using CMAppBusiness.Services;
using System;
using System.Collections.Generic;
using CMAppDataLayer;
using System.Linq;

namespace CMAppBusiness.Services
{
    public class ProductService : BaseService, IProductService
    {
        public List<Product> GetCategory(string catName)
        {
            List<Product> products;
            if (catName == "")
            {
                products = _ctx.Products.ToList<Product>();
            }
            else
            {
                products = _ctx.Products.Where(p => p.Category == catName).ToList<Product>();
            }
            return products;
        }
        public void addToCart(int pId)
        {
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
        public List<CMAppDataLayer.Models.Orders> getOrdersData()
        {
            List<Product> Products = _ctx.Products.ToList<Product>();
            List<Customer> Customers = _ctx.Customers.ToList<Customer>();
            List<Order> Orders = _ctx.Orders.ToList<Order>();
            List<Order_Products> Order_Products = _ctx.Order_Products.ToList<Order_Products>();

            var ordersData = (from cust in Customers
                              join o in Orders on cust.CID equals o.CID
                              join op in Order_Products on o.OrderID equals op.OrderID
                              join p in Products on op.PID equals p.PID
                              select new CMAppDataLayer.Models.Orders
                              {
                                  FName = cust.FName,
                                  LName = cust.LName,
                                  Phone = cust.Phone,
                                  OrderDate = o.OrderDate.ToString("dd/MM/yyyy"),
                                  DeliveryDate = o.DeliveryDate.ToString("dd/MM/yyyy"),
                                  PName = p.PName,
                                  Qty = op.Qty.ToString(),
                                  TotalSale = op.TotalSale.ToString(),
                                  Brand = p.Brand
                              }).ToList();
            return ordersData;
        }
    }
}
