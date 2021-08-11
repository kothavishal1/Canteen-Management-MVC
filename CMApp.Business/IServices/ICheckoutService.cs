using CMAppDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMApp.Business.IServices
{
    public interface ICheckoutService
    {
        int checkOut(int type, int pId);
        decimal UpdateTotal();
        void Clear();
        void Purchase(Customer customer);
    }
}
