using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentOfEquipment.ClassHelper
{
    public class Helper
    {
        public static EF.Client ClientInfo { get; set; }
        public static EF.Product EquipmentInfo { get; set; }

        public static EF.Passport PassportInfo { get; set; }
    }
}
