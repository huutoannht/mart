using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_Test.DT
{
    public class Product
    {

        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string UnitPrice { get; set; }
        public string UnitPriceNew { get; set; }
        public string CategoryID { get; set; }
        public string MetaKeywords { get; set; }
        public string Rating { get; set; }

    }
}
