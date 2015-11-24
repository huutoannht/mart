using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_Test.DT
{
    public class Product3OutputScDT
    {
        /// <summary>
        /// リターンコード
        /// </summary>
        public string O_RETURN_CD { get; set; }
        /// <summary>
        /// 候補顧客リスト
        /// </summary>
        public List<Product> O_CANDIDATE_CUSTOMER_LIST { get; set; }
    }
}
