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
            Product3OutputScDT Product3OutputScDT = new Product3OutputScDT();
            List<ResponseDTO> lstResponse = new List<ResponseDTO>();
            try
            {
                CheckParameter(ProductInputDT);
                Product3OutputScDT.O_CANDIDATE_CUSTOMER_LIST =ProductDA.GetInfo(ProductInputDT);
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
        /// <summary>
        /// Function check validate parameter input
        /// </summary>
        /// <param name="ProductInputDT"></param>
        private void CheckParameter(RequestDTO ProductInputDT)
        {
            //Check validate max length
            if (ProductInputDT.I_TEMPO_CD !=null && ProductInputDT.I_TEMPO_CD.Length > 6)
            { 
                throw  new SqlExceptionAPI(ItemIdCS.E003);
            }
            if (ProductInputDT.I_KANJI_SEI !=null && ProductInputDT.I_KANJI_SEI.Length > 50)
            {
                throw new SqlExceptionAPI(ItemIdCS.E003);
            }
            if (ProductInputDT.I_KANJI_MEI !=null && ProductInputDT.I_KANJI_MEI.Length > 50)
            {
                throw new SqlExceptionAPI(ItemIdCS.E003);
            }
            if (ProductInputDT.I_KANA_SEI!=null && ProductInputDT.I_KANA_SEI.Length > 50)
            {
                throw new SqlExceptionAPI(ItemIdCS.E003);
            }
            if (ProductInputDT.I_KANA_MEI !=null && ProductInputDT.I_KANA_MEI.Length > 50)
            {
                throw new SqlExceptionAPI(ItemIdCS.E003);
            }
            //if (ProductInputDT.I_ZIP!=null && ProductInputDT.I_ZIP.Length > 7)
            //{
            //    throw new SqlExceptionAPI(ItemIdCS.E003);
            //}
            if (ProductInputDT.I_JUSHO1 !=null && ProductInputDT.I_JUSHO1.Length > 100)
            {
                throw new SqlExceptionAPI(ItemIdCS.E003);
            }
            if (ProductInputDT.I_JUSHO2 !=null && ProductInputDT.I_JUSHO2.Length > 100)
            {
                throw new SqlExceptionAPI(ItemIdCS.E003);
            }
            if (ProductInputDT.I_TEL !=null && ProductInputDT.I_TEL.Length > 13)
            {
                throw new SqlExceptionAPI(ItemIdCS.E003);
            }
            if (ProductInputDT.I_TEL_KEITAI !=null && ProductInputDT.I_TEL_KEITAI.Length > 13)
            {
                throw new SqlExceptionAPI(ItemIdCS.E003);
            }
            //if (ProductInputDT.I_FAX !=null && ProductInputDT.I_FAX.Length > 13)
            //{
            //    throw new SqlExceptionAPI(ItemIdCS.E003);
            //}
            if (ProductInputDT.I_MAIL !=null && ProductInputDT.I_MAIL.Length > 100)
            {
                throw new SqlExceptionAPI(ItemIdCS.E003);
            }
            if (ProductInputDT.I_MAIL_KEITAI!=null && ProductInputDT.I_MAIL_KEITAI.Length > 100)
            {
                throw new SqlExceptionAPI(ItemIdCS.E003);
            }
            //Check validate datetime 
            DateTime dateTime;
            if (!string.IsNullOrEmpty(ProductInputDT.I_SEINENGAPPI)&&!DateTime.TryParse(ProductInputDT.I_SEINENGAPPI,out dateTime)){
                DateTime result;
                if (!DateTime.TryParseExact(
                     ProductInputDT.I_SEINENGAPPI,
                     "yyyyMMdd",
                     CultureInfo.InvariantCulture,
                     DateTimeStyles.AssumeUniversal,
                     out result))
                {
                    throw new SqlExceptionAPI(ItemIdCS.E003);
                };
            }
            //try
            //{
            //    if (!string.IsNullOrEmpty(ProductInputDT.I_SEX))
            //    {
            //        Int32.Parse(ProductInputDT.I_SEX.ToString());
            //    }
            //    if (!string.IsNullOrEmpty(ProductInputDT.I_TODOFUKEN_CD))
            //    {
            //        Int32.Parse(ProductInputDT.I_TODOFUKEN_CD.ToString());
            //    }
            //}
            //catch (Exception)
            //{
            //    throw new SqlExceptionAPI(ItemIdCS.E003);
            //}
        }
        #endregion
        
    }
}
