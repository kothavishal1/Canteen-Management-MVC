using CMApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMAppDataLayer;
namespace CMApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BaseController : Controller
    {
        protected CanteenManagementEntities _ctx = new CanteenManagementEntities();
    }
}