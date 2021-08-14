using CMAppDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAppBusiness.Services
{
    public class BaseService
    {
        protected CanteenManagementEntities _ctx = new CanteenManagementEntities();
    }
}
