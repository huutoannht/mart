using api_Test.CM;
using api_Test.DT;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_Test.BS
{
    public class Product3BS
    {
        #region Variables
        ProductDA ProductDA = null;
        Product3OutputFaDT responseFail = null;
        #endregion
        #region Functions
        /// <summary>
        /// Function get infor with input paramter
        /// </summary>
        /// <param name="ProductInputDT"></param>
        /// <returns></returns>
        public object GetInfo(RequestDTO ProductInputDT)
        {
            ProductDA = new ProductDA();
            ProductOutputScDT Product3OutputScDT = new ProductOutputScDT();
            List<ResponseDTO> lstResponse = new List<ResponseDTO>();
            try
            {
                Product3OutputScDT.O_PRODUCT_LIST = ProductDA.GetAllProduct(ProductInputDT);
                Product3OutputScDT.O_RETURN_CD = ItemIdCS.O_RETURN_CD_SUCCESS;    
            }
            catch (Exception ex)
            {
                responseFail = new Product3OutputFaDT();
                if (ItemIdCS.E001.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E001;
                    responseFail.O_ERROR_INFO = MsgCS.E001;
                    return  responseFail;
                }
                else if (ItemIdCS.E002.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E002;
                    responseFail.O_ERROR_INFO = MsgCS.E002;
                    return responseFail;
                }
                else if (ItemIdCS.E003.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E003;
                    responseFail.O_ERROR_INFO = MsgCS.E003;
                    return responseFail;
                }
                else if (ItemIdCS.E004.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E004;
                    responseFail.O_ERROR_INFO = MsgCS.E004;
                    return responseFail;
                }
                else
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E999;
                    responseFail.O_ERROR_INFO = MsgCS.E999;
                    return responseFail;
                }
            }
            return Product3OutputScDT;
        }
        /// <summary>
        /// Function get infor with input paramter
        /// </summary>
        /// <param name="ProductInputDT"></param>
        /// <returns></returns>
        public object GetProductById(int productId)
        {
            ProductDA = new ProductDA();
            ProductOutputScDT Product3OutputScDT = new ProductOutputScDT();
            List<ResponseDTO> lstResponse = new List<ResponseDTO>();
            try
            {
                Product3OutputScDT.O_PRODUCT_LIST = ProductDA.GetProductById(productId);
                Product3OutputScDT.O_RETURN_CD = ItemIdCS.O_RETURN_CD_SUCCESS;
            }
            catch (Exception ex)
            {
                responseFail = new Product3OutputFaDT();
                if (ItemIdCS.E001.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E001;
                    responseFail.O_ERROR_INFO = MsgCS.E001;
                    return responseFail;
                }
                else if (ItemIdCS.E002.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E002;
                    responseFail.O_ERROR_INFO = MsgCS.E002;
                    return responseFail;
                }
                else if (ItemIdCS.E003.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E003;
                    responseFail.O_ERROR_INFO = MsgCS.E003;
                    return responseFail;
                }
                else if (ItemIdCS.E004.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E004;
                    responseFail.O_ERROR_INFO = MsgCS.E004;
                    return responseFail;
                }
                else
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E999;
                    responseFail.O_ERROR_INFO = MsgCS.E999;
                    return responseFail;
                }
            }
            return Product3OutputScDT;
        }
        
        #endregion
        
    }
}
