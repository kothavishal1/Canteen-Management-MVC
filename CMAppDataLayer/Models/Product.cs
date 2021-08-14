using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMAppDataLayer
{
    public partial class Product
    {
        private CanteenManagementEntities _ctx = new CanteenManagementEntities();
        public List<Product> All
        {
            get
            {
                return _ctx.Products.ToList<Product>();

            }
        }
    }
}