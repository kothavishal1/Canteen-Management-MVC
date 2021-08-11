using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAppBusiness.IServices
{
    public interface IProductService
    {
        List<CMAppDataLayer.Product> GetCategory(string catName);
        void addToCart(int pId);
        List<CMAppDataLayer.Models.Orders> getOrdersData();
    }
}
