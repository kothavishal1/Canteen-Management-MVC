﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMApp.Models
{
    public class OrderModel : CMApp.Order
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