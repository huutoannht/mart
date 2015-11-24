using api_Test.DT;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Models.DataAccess.CommonDA;

namespace api_Test.CM
{
    public class ProductDA
    {
        #region Variables
   
        static protected log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        int timeout = 300;
        #endregion
        #region Functions
        public List<Product> GetAllProduct(RequestDTO Product3InputDT)
        {
            #region Variables
            try
            {
                timeout = Int32.Parse(ConfigurationManager.AppSettings.Get("CommandTimeout"));
            }
            catch
            {
                timeout=300;
            } 
            SqlConnection connection = DBManager.Connection(ProductConst.CONSTRMART);
            List<Product> lstProduct = new List<Product>();
            #endregion
            #region GetSQL
            String query = @"  select  *
                                from Products ";

            

            /*END*/

            #endregion

            try
            {
                connection.Open();
             

                IDataReader dataReader = DBManager.GetData(query: query, parameter: null, conn: connection, connectTimeout: timeout);
                while (dataReader.Read())
                {
                    Product product = new Product();
                    product.CategoryID = DBNull.Value.Equals(dataReader["CategoryID"]) ? null : dataReader["CategoryID"].ToString();
                    product.Description = DBNull.Value.Equals(dataReader["Description"]) ? null : dataReader["Description"].ToString();
                    product.ImagePath = DBNull.Value.Equals(dataReader["ImagePath"]) ? null : dataReader["ImagePath"].ToString();
                    product.MetaKeywords = DBNull.Value.Equals(dataReader["MetaKeywords"]) ? null : dataReader["MetaKeywords"].ToString();
                    product.ProductID = DBNull.Value.Equals(dataReader["ProductID"]) ? (int?)null : Int32.Parse(dataReader["ProductID"].ToString());
                    product.ProductName = DBNull.Value.Equals(dataReader["ProductName"]) ? null : dataReader["ProductName"].ToString();
                    product.Rating = DBNull.Value.Equals(dataReader["Rating"]) ? null : dataReader["Rating"].ToString();
                    product.UnitPrice = DBNull.Value.Equals(dataReader["UnitPrice"]) ? null : dataReader["UnitPrice"].ToString();
                    product.UnitPriceNew = DBNull.Value.Equals(dataReader["UnitPriceNew"]) ? null : dataReader["UnitPriceNew"].ToString();
                    lstProduct.Add(product);
                }
            }
            catch (SqlException ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                if (ex.Number == -2)
                {
                    throw new SqlExceptionAPI(ItemIdCS.E001);
                }
                if (ex.Number == 2 || ex.Number == -1 || ex.Number == 18456 || ex.Number == 4060)
                {
                    throw new SqlExceptionAPI(ItemIdCS.E002);
                }
                else
                {
                    throw new SqlExceptionAPI(ItemIdCS.E004);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw new SqlExceptionAPI(ItemIdCS.E999);
            }
            finally
            {
                connection.Close();
            }
            return lstProduct;
        }
        public List<Product> GetProductById(int  productId)
        {
            #region Variables
            try
            {
                timeout = Int32.Parse(ConfigurationManager.AppSettings.Get("CommandTimeout"));
            }
            catch
            {
                timeout = 300;
            }
            SqlConnection connection = DBManager.Connection(ProductConst.CONSTRMART);
            List<Product> lstProduct = new List<Product>();
            #endregion
            #region GetSQL
            String query = @"  select  *
                                from Products 

                                WHERE ProductId = @ProductId";
            #endregion

            try
            {
                connection.Open();
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@ProductId",productId)
                };

                IDataReader dataReader = DBManager.GetData(query: query, parameter: parameters, conn: connection, connectTimeout: timeout);
                while (dataReader.Read())
                {
                    Product product = new Product();
                    product.CategoryID = DBNull.Value.Equals(dataReader["CategoryID"]) ? null : dataReader["CategoryID"].ToString();
                    product.Description = DBNull.Value.Equals(dataReader["Description"]) ? null : dataReader["Description"].ToString();
                    product.ImagePath = DBNull.Value.Equals(dataReader["ImagePath"]) ? null : dataReader["ImagePath"].ToString();
                    product.MetaKeywords = DBNull.Value.Equals(dataReader["MetaKeywords"]) ? null : dataReader["MetaKeywords"].ToString();
                    product.ProductID = DBNull.Value.Equals(dataReader["ProductID"]) ? (int?)null : Int32.Parse(dataReader["ProductID"].ToString());
                    product.ProductName = DBNull.Value.Equals(dataReader["ProductName"]) ? null : dataReader["ProductName"].ToString();
                    product.Rating = DBNull.Value.Equals(dataReader["Rating"]) ? null : dataReader["Rating"].ToString();
                    product.UnitPrice = DBNull.Value.Equals(dataReader["UnitPrice"]) ? null : dataReader["UnitPrice"].ToString();
                    product.UnitPriceNew = DBNull.Value.Equals(dataReader["UnitPriceNew"]) ? null : dataReader["UnitPriceNew"].ToString();
                    lstProduct.Add(product);
                }
            }
            catch (SqlException ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                if (ex.Number == -2)
                {
                    throw new SqlExceptionAPI(ItemIdCS.E001);
                }
                if (ex.Number == 2 || ex.Number == -1 || ex.Number == 18456 || ex.Number == 4060)
                {
                    throw new SqlExceptionAPI(ItemIdCS.E002);
                }
                else
                {
                    throw new SqlExceptionAPI(ItemIdCS.E004);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw new SqlExceptionAPI(ItemIdCS.E999);
            }
            finally
            {
                connection.Close();
            }
            return lstProduct;
        }

        #endregion
    }
}
