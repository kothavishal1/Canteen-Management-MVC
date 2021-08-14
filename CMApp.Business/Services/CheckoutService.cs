using CMApp.Business.IServices;
using CMAppBusiness.Services;
using CMAppDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMApp.Business.Services
{
    public class CheckoutService : BaseService, ICheckoutService
    {
        public int checkOut(int type, int pId)
        {
            CanteenManagementEntities context = new CanteenManagementEntities();

            ShoppingCartData product = context.ShoppingCartDatas.FirstOrDefault(p => p.PID == pId);
            if (product == null)
            {
                return 0;
            }

            Product actualProduct = context.Products.FirstOrDefault(p => p.PID == pId);
            int quantity;
            // if type 0, decrease quantity
            // if type 1, increase quanity
            switch (type)
            {
                case 0:
                    product.Quantity--;
                    actualProduct.UnitsInStock++;
                    break;
                case 1:
                    product.Quantity++;
                    actualProduct.UnitsInStock--;
                    break;
                case -1:
                    actualProduct.UnitsInStock += product.Quantity;
                    product.Quantity = 0;
                    break;
                default:
                    return 0;
            }

            if (product.Quantity == 0)
            {
                context.ShoppingCartDatas.Remove(product);
                quantity = 0;
            }
            else
            {
                quantity = product.Quantity;
            }

            context.SaveChanges();
            return quantity;
        }
        public decimal UpdateTotal()
        {
            CanteenManagementEntities context = new CanteenManagementEntities();
            decimal total = 0;
            try
            {
                total = context.ShoppingCartDatas.Select(p => p.UnitPrice * p.Quantity).Sum();
            }
            catch (Exception exx)
            {
                total = 0;
            }
            return total;
        }

        public void Clear()
        {
            List<ShoppingCartData> carts = _ctx.ShoppingCartDatas.ToList();
            carts.ForEach(a => {
                Product product = _ctx.Products.FirstOrDefault(p => p.PID == a.PID);
                product.UnitsInStock += a.Quantity;
            });
            _ctx.ShoppingCartDatas.RemoveRange(carts);
            _ctx.SaveChanges();
        }
        public void Purchase(Customer customer)
        {
            Customer c = new Customer
            {
                FName = customer.FName,
                LName = customer.LName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address1 = customer.Address1,
                Address2 = customer.Address2,
                Suburb = customer.Suburb,
                Postcode = customer.Postcode,
                State = customer.State,
                Ctype = customer.Ctype,
                CardNo = customer.CardNo,
                ExpDate = customer.ExpDate
            };

            Order o = new Order
            {
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now.AddDays(5),
                CID = c.CID
            };

            _ctx.Customers.Add(c);
            _ctx.Orders.Add(o);

            foreach (var i in _ctx.ShoppingCartDatas.ToList<ShoppingCartData>())
            {
                _ctx.Order_Products.Add(new Order_Products
                {
                    OrderID = o.OrderID,
                    PID = i.PID,
                    Qty = i.Quantity,
                    TotalSale = i.Quantity * i.UnitPrice
                });
                _ctx.ShoppingCartDatas.Remove(i);
            }

            _ctx.SaveChanges();
        }
    }
}
