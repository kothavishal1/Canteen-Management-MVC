using CMApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMAppDataLayer;
using CMApp.Business.Services;
using CMApp.Business.IServices;

namespace CMApp.Controllers
{
    public class CheckoutController : BaseController
    {
        private List<object> states;
        private List<object> cards;
        private readonly ICheckoutService _checkOut;
        public CheckoutController()
        {
            states = new List<object> {
                new { SID = "NSW", SName = "New South Wales" },
                new { SID = "VIC", SName = "Victoria" },
                new { SID = "QLD", SName = "Queensland" },
                new { SID = "TAs", SName = "Tasmania" },
                new { SID = "NT", SName = "Northern Territory" },
                new { SID = "SA", SName = "South Australia" },
                new { SID = "WA", SName = "Western Australia" },
                new { SID = "ACT", SName = "Australian Capital Territory" }
            };

            cards = new List<object> {
                new { Type = "VISA" },
                new { Type = "Master Card" },
                new { Type = "AMEX" }
            };
            _checkOut = new CheckoutService();
        }
        
        // GET: Checkout
        public ActionResult Index()
        {
            ViewBag.Cart = _ctx.ShoppingCartDatas.ToList<ShoppingCartData>();
            return View();
        }
        
        public JsonResult QuanityChange(int type, int pId)
        {
            int quantity = _checkOut.checkOut(type, pId);
            return Json(new { d = quantity });
        }
        
        [HttpGet]
        public JsonResult UpdateTotal()
        {
            decimal total = _checkOut.UpdateTotal();
            return Json(new { d = String.Format(System.Globalization.CultureInfo.GetCultureInfo("en-GB"), "{0:c}", total) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Clear()
        {
            try
            {
               _checkOut.Clear();
            }
            catch (Exception) { }
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult Purchase()
        {
            ViewBag.States = states;
            ViewBag.Cards = cards;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase(Customer customer)
        {
            ViewBag.States = states;
            ViewBag.Cards = cards;

            if (ModelState.IsValid)
            {
                if (customer.ExpDate <= DateTime.Now)
                {
                    ModelState.AddModelError("", "Credit card has already expired");
                }

                if (customer.Ctype == "AMEX")
                {
                    if (customer.CardNo.Length != 15)
                    {
                        ModelState.AddModelError("", "AMEX must be 15 digits");
                    }
                }
                else
                {
                    if (customer.CardNo.Length != 16)
                    {
                        ModelState.AddModelError("", customer.Ctype + "must be 16 digits");
                    }
                }

                if (ModelState.IsValid)
                {

                    _checkOut.Purchase(customer);
                    return RedirectToAction("PurchasedSuccess");

                }
            }

            List<ModelError> errors = new List<ModelError>();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors.Add(error);
                }
            }
            return View(customer);
        }

        public ActionResult PurchasedSuccess()
        {
            return View();
        }
    }
}
