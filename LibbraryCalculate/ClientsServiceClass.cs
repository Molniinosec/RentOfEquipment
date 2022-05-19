using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibbraryCalculate
{
    public  class ClientsServiceClass
    {
        
        public static decimal TotalCost(DateTime start, DateTime end, double cost)
        {
            TimeSpan time = (end - start);
            int timer = Convert.ToInt32(time.TotalDays);
            decimal result = Math.Round(Convert.ToDecimal((Convert.ToDouble(cost) * 0.05) * (timer + 1)), 2);        
            return result;
        }

        public static void VearOfProduct()
        {
            return;
        }
    }
}
