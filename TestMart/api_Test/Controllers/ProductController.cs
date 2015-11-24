using api_Test.BS;
using api_Test.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace api_Test.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        #region Variables
        static protected log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        
        [HttpGet]
        [Route("GetProducts")]
        public async Task<object> GetProduct(RequestDTO requestDTO)
        {
            Product3BS ProductBS = new Product3BS();
            var result = await Task.Run(() => ProductBS.GetInfo(requestDTO));
            return result;
        }
        [HttpGet]
        [Route("GetProductById/{id:int}")]
        public async Task<object> GetProductById(int id)
        {
            Product3BS ProductBS = new Product3BS();
            var result = await Task.Run(() => ProductBS.GetProductById(id));
            return result;
        }
    }
}
