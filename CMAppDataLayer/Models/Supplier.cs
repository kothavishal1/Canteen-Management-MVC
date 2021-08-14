using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMAppDataLayer
{
    public partial class Supplier
    {
        private CanteenManagementEntities _ctx = new CanteenManagementEntities();

        public IEnumerable<Supplier> All
        {
            get
            {
                return _ctx.Suppliers;
            }
        }
    }
}