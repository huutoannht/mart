using api_Test.BS;
using api_Test.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api_Test.Controllers
{
    public class GetApsCustomerListController : ApiController
    {
        #region Variables
        static protected log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        
        [HttpPost]
        public object Post(RequestDTO requestDTO)
        {
            Product3BS ProductBS = new Product3BS();
            return ProductBS.GetInfo(requestDTO);     
        }
    }
}
