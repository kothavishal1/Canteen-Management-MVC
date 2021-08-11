using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMAppDataLayer;

namespace CMAppDataLayer.Models.Admin
{
    public class OrderModel : Order
    {

        public decimal TotalPayment
        {
            get
            {
                return this.Order_Products.Sum(p => p.TotalSale);
            }
        }
    }
}