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
        public List<ResponseDTO> GetInfo(RequestDTO Product3InputDT)
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
            SqlConnection connection = DBManager.Connection(Product3Const.CONSTRTAPS);
            List<ResponseDTO> lstResponseDTO = new List<ResponseDTO>();
            #endregion
            #region GetSQL
            String query = @"       SELECT *
                                    FROM (
                                    SELECT
                                                    TMP.*
                                            ,APP.TEMPO_CD AS FUTAI_TEMPO_CD
                                            ,APP.SHODAN_KANRI_NO AS FUTAI_SHODAN_KANRI_NO
                                            ,APP.SHODAN_KANRI_EDA_NO AS FUTAI_SHODAN_EDA_NO
                                            ,CASE APP.SHODAN_SHUBETSU_CD 
                                                            WHEN 3 THEN '付帯'
                                                            WHEN 2 THEN '販売'
                                                            WHEN 1 THEN '買取'
                                                            ELSE NULL
                                                    END AS FUTAI_SHODAN_SHUBETSU
                                            ,STS.SHODAN_BUNRUI_NAME AS FUTAI_SHODAN_BUNRUI
                                            ,APP.SHOKAI_SHODAN_YMD AS FUTAI_SHOKAI_SHODAN_YMD
                                            ,ROW_NUMBER()OVER(
                                                    PARTITION BY TMP.KOKYAKU_NO
                                                    ORDER BY APP.SHOKAI_SHODAN_YMD DESC
                                                    )AS ROW_NUM3
                                    FROM (
                                    SELECT
                                             TMP.*
                                            ,SNG.TEMPO_CD AS HAMBAI_TEMPO_CD
                                            ,SNG.SHODAN_KANRI_NO AS HAMBAI_SHODAN_KANRI_NO
                                            ,SNG.SHODAN_KANRI_EDA_NO AS HAMBAI_SHODAN_EDA_NO
                                            ,CASE SNG.SHODAN_SHUBETSU_CD 
                                                            WHEN 3 THEN '付帯'
                                                            WHEN 2 THEN '販売'
                                                            WHEN 1 THEN '買取'
                                                            ELSE NULL
                                                    END AS HAMBAI_SHODAN_SHUBETSU
                                            ,STS.SHODAN_BUNRUI_NAME AS HAMBAI_SHODAN_BUNRUI
                                            ,SNG.SHOKAI_SHODAN_YMD AS HAMBAI_SHOKAI_SHODAN_YMD
                                            ,ROW_NUMBER() OVER (
                                                    PARTITION BY TMP.KOKYAKU_NO
                                                    ORDER BY SNG.SHOKAI_SHODAN_YMD DESC
                                                    ) AS ROW_NUM2
                                    FROM (
                                    SELECT
                                             CST.*
                                            ,PNG.TEMPO_CD AS KAITORI_TEMPO_CD
                                            ,PNG.SHODAN_KANRI_NO AS KAITORI_SHODAN_KANRI_NO
                                            ,PNG.SHODAN_KANRI_EDA_NO AS KAITORI_SHODAN_EDA_NO
                                            ,CASE PNG.SHODAN_SHUBETSU_CD 
                                                            WHEN 3 THEN '付帯'
                                                            WHEN 2 THEN '販売'
                                                            WHEN 1 THEN '買取'
                                                            ELSE NULL
                                                    END AS KAITORI_SHODAN_SHUBETSU
                                            ,STS.SHODAN_BUNRUI_NAME AS KAITORI_SHODAN_BUNRUI
                                            ,PNG.SHOKAI_SHODAN_YMD AS KAITORI_SHOKAI_SHODAN_YMD
                                            ,ROW_NUMBER() OVER (
                                                    PARTITION BY CST.KOKYAKU_NO
                                                    ORDER BY PNG.SHOKAI_SHODAN_YMD DESC
                                                    ) AS ROW_NUM
                                    FROM (
                                    SELECT TOP 50
                                             RTRIM(CUST.KOKYAKU_NO) KOKYAKU_NO        -- 顧客番号
                                            ,CUST.KOKYAKU_KANJI_SEI                                -- 顧客名漢字姓
                                            ,CUST.KOKYAKU_KANJI_MEI                                -- 顧客名漢字名
                                            ,CUST.KOKYAKU_KANA_SEI                                -- 顧客名カナ姓
                                            ,CUST.KOKYAKU_KANA_MEI                                -- 顧客名カナ名
                                            ,CUST.KOKYAKU_SHUBETSU_CD                        -- 顧客種別コード
                                            ,SHUBETSU.KOKYAKU_SHUBETSU                        -- 顧客種別名称
                                            ,CUST.QUESTIONNAIRE_KINYU_FLG                -- アンケート記入フラグ
                                            ,CUST.SEIBETSU_CD                                        -- 性別コード
                                            ,CUST.SEINENGAPPI                                        -- 生年月日
                                            ,CUST.NENREI
                                            ,RTRIM(CUST.ZIP) ZIP                                -- 郵便番号
                                            ,CUST.TODOFUKEN_CD                                        -- 都道府県コード
                                            ,KEN.KEN_NAME TODOFUKEN_NAME                -- 都道府県名
                                            ,CUST.JUSHO1
                                            ,CUST.JUSHO2
                                            ,RTRIM(CUST.TEL) TEL                                -- 電話番号
                                            ,RTRIM(CUST.TEL_KEITAI) TEL_KEITAI        -- 携帯番号
                                            ,RTRIM(CUST.FAX_NO) FAX_NO                        -- FAX番号
                                            ,CUST.MAIL
                                            ,CUST.MAIL_KEITAI                                        -- 携帯メール
                                            ,CUST.RENRAKU_FLG                                        -- 連絡フラグ
                                            ,CUST.FUTSU_FLG
                                            ,CUST.RENRAKU_FUNO_YMD                                -- 連絡不能日時
                                            ,CUST.BIKO
                                            ,CUST.DELETE_DATE                                        -- 削除日
                                            ,CUST.CREATE_DATE                                        -- 登録日時
                                            ,CUST.CREATE_USER_ID                                -- 登録者コード
                                            ,CUST.UPDATE_DATE                                        -- 更新日時
                                            ,CUST.UPDATE_USER_ID                                -- 更新者コード
                                            ,CUST.VersionNo
				                            ,RTRIM(CON.TEMPO_CD) TEMPO_CD -- 店舗コード                                
			                            FROM TT_KOKYAKU CUST(NOLOCK)
			                            -- TM_顧客種別                                                
			                            LEFT OUTER JOIN TM_KOKYAKU_SHUBETSU SHUBETSU(NOLOCK) ON SHUBETSU.KOKYAKU_SHUBETSU_CD = CUST.KOKYAKU_SHUBETSU_CD
			                            -- TM_県                                                
			                            LEFT OUTER JOIN TM_KEN KEN(NOLOCK) ON KEN.KEN_CD = CUST.TODOFUKEN_CD
			                            -- TT_顧客商談紐付
			                            LEFT OUTER JOIN TT_CONNECT_SHODAN_KOKYAKU CON(NOLOCK) ON CON.KOKYAKU_NO = CUST.KOKYAKU_NO
				                            AND CON.KOKYAKU_ZOKUSEI = 99
				                            AND CON.DELETE_DATE IS NULL
			                            WHERE CUST.DELETE_DATE IS NULL ";

            

            /*END*/

            #endregion

            try
            {
                connection.Open();
             

                IDataReader dataReader = DBManager.GetData(query: query, parameter: null, conn: connection, connectTimeout: timeout);
                while (dataReader.Read())
                {
                    ResponseDTO responseDTO = new ResponseDTO();
                    responseDTO.O_KOKYAKU_NO = DBNull.Value.Equals(dataReader["KOKYAKU_NO"]) ? null : dataReader["KOKYAKU_NO"].ToString();
                    responseDTO.O_KANJI_SEI = DBNull.Value.Equals(dataReader["KOKYAKU_KANJI_SEI"]) ? null : dataReader["KOKYAKU_KANJI_SEI"].ToString();
                    responseDTO.O_KANJI_MEI = DBNull.Value.Equals(dataReader["KOKYAKU_KANJI_MEI"]) ? null : dataReader["KOKYAKU_KANJI_MEI"].ToString();
                    responseDTO.O_KANA_SEI = DBNull.Value.Equals(dataReader["KOKYAKU_KANA_SEI"]) ? null : dataReader["KOKYAKU_KANA_SEI"].ToString();
                    responseDTO.O_KANA_MEI = DBNull.Value.Equals(dataReader["KOKYAKU_KANA_MEI"]) ? null : dataReader["KOKYAKU_KANA_MEI"].ToString();
                    responseDTO.O_SEX = DBNull.Value.Equals(dataReader["SEIBETSU_CD"]) ? (int?)null : Int32.Parse(dataReader["SEIBETSU_CD"].ToString());
                    responseDTO.O_SEINENGAPPI = DBNull.Value.Equals(dataReader["SEINENGAPPI"]) ? null : dataReader["SEINENGAPPI"].ToString();
                    responseDTO.O_ZIP = DBNull.Value.Equals(dataReader["ZIP"]) ? null : dataReader["ZIP"].ToString();
                    responseDTO.O_TODOFUKEN_CD = DBNull.Value.Equals(dataReader["TODOFUKEN_CD"]) ? (int?)null : Int32.Parse(dataReader["TODOFUKEN_CD"].ToString());
                    responseDTO.O_JUSHO1 = DBNull.Value.Equals(dataReader["JUSHO1"]) ? null : dataReader["JUSHO1"].ToString();
                    responseDTO.O_JUSHO2 = DBNull.Value.Equals(dataReader["JUSHO2"]) ? null : dataReader["JUSHO2"].ToString();
                    responseDTO.O_TEL = DBNull.Value.Equals(dataReader["TEL"]) ? null : dataReader["TEL"].ToString();
                    responseDTO.O_TEL_KEITAI = DBNull.Value.Equals(dataReader["TEL_KEITAI"]) ? null : dataReader["TEL_KEITAI"].ToString();
                    responseDTO.O_FAX = DBNull.Value.Equals(dataReader["FAX_NO"]) ? null : dataReader["FAX_NO"].ToString();
                    responseDTO.O_MAIL = DBNull.Value.Equals(dataReader["MAIL"]) ? null : dataReader["MAIL"].ToString();
                    responseDTO.O_MAIL_KEITAI = DBNull.Value.Equals(dataReader["MAIL_KEITAI"]) ? null : dataReader["MAIL_KEITAI"].ToString();
                    responseDTO.O_TEMPO_CD = DBNull.Value.Equals(dataReader["TEMPO_CD"]) ? null : dataReader["TEMPO_CD"].ToString();

                    responseDTO.O_HAMBAI_TEMPO_CD = DBNull.Value.Equals(dataReader["HAMBAI_TEMPO_CD"]) ? null : dataReader["HAMBAI_TEMPO_CD"].ToString();
                    responseDTO.O_HAMBAI_SHODAN_KANRI_NO = DBNull.Value.Equals(dataReader["HAMBAI_SHODAN_KANRI_NO"]) ? null : dataReader["HAMBAI_SHODAN_KANRI_NO"].ToString();
                    responseDTO.O_HAMBAI_SHODAN_KANRI_EDA_NO = DBNull.Value.Equals(dataReader["HAMBAI_SHODAN_EDA_NO"]) ? (int?)null : Int32.Parse(dataReader["HAMBAI_SHODAN_EDA_NO"].ToString());
                    responseDTO.O_HAMBAI_SHODAN_SHUBETSU = DBNull.Value.Equals(dataReader["HAMBAI_SHODAN_SHUBETSU"]) ? null : dataReader["HAMBAI_SHODAN_SHUBETSU"].ToString();
                    responseDTO.O_HAMBAI_SHODAN_BUNRUI = DBNull.Value.Equals(dataReader["HAMBAI_SHODAN_BUNRUI"]) ? null : dataReader["HAMBAI_SHODAN_BUNRUI"].ToString();
                    responseDTO.O_HAMBAI_SHOKAI_SHODAN_YMD = DBNull.Value.Equals(dataReader["HAMBAI_SHOKAI_SHODAN_YMD"]) ? null : dataReader["HAMBAI_SHOKAI_SHODAN_YMD"].ToString();
                    responseDTO.O_KAITORI_TEMPO_CD = DBNull.Value.Equals(dataReader["KAITORI_TEMPO_CD"]) ? null : dataReader["KAITORI_TEMPO_CD"].ToString();
                    responseDTO.O_KAITORI_SHODAN_KANRI_NO = DBNull.Value.Equals(dataReader["KAITORI_SHODAN_KANRI_NO"]) ? null : dataReader["KAITORI_SHODAN_KANRI_NO"].ToString();
                    responseDTO.O_KAITORI_SHODAN_KANRI_EDA_NO = DBNull.Value.Equals(dataReader["KAITORI_SHODAN_EDA_NO"]) ? (int?)null : Int32.Parse(dataReader["KAITORI_SHODAN_EDA_NO"].ToString());
                    responseDTO.O_KAITORI_SHODAN_SHUBETSU = DBNull.Value.Equals(dataReader["KAITORI_SHODAN_SHUBETSU"]) ? null : dataReader["KAITORI_SHODAN_SHUBETSU"].ToString();
                    responseDTO.O_KAITORI_SHODAN_BUNRUI = DBNull.Value.Equals(dataReader["KAITORI_SHODAN_BUNRUI"]) ? null : dataReader["KAITORI_SHODAN_BUNRUI"].ToString();
                    responseDTO.O_KAITORI_SHOKAI_SHODAN_YMD = DBNull.Value.Equals(dataReader["KAITORI_SHOKAI_SHODAN_YMD"]) ? null : dataReader["KAITORI_SHOKAI_SHODAN_YMD"].ToString();

                    responseDTO.O_FUTAI_TEMPO_CD = DBNull.Value.Equals(dataReader["FUTAI_TEMPO_CD"]) ? null : dataReader["FUTAI_TEMPO_CD"].ToString();
                    responseDTO.O_FUTAI_SHODAN_KANRI_NO = DBNull.Value.Equals(dataReader["FUTAI_SHODAN_KANRI_NO"]) ? null : dataReader["FUTAI_SHODAN_KANRI_NO"].ToString();
                    responseDTO.O_FUTAI_SHODAN_KANRI_EDA_NO = DBNull.Value.Equals(dataReader["FUTAI_SHODAN_EDA_NO"]) ? (int?)null : Int32.Parse(dataReader["FUTAI_SHODAN_EDA_NO"].ToString());
                    responseDTO.O_FUTAI_SHODAN_SHUBETSU = DBNull.Value.Equals(dataReader["FUTAI_SHODAN_SHUBETSU"]) ? null : dataReader["FUTAI_SHODAN_SHUBETSU"].ToString();
                    responseDTO.O_FUTAI_SHODAN_BUNRUI = DBNull.Value.Equals(dataReader["FUTAI_SHODAN_BUNRUI"]) ? null : dataReader["FUTAI_SHODAN_BUNRUI"].ToString();
                    responseDTO.O_FUTAI_SHOKAI_SHODAN_YMD = DBNull.Value.Equals(dataReader["FUTAI_SHOKAI_SHODAN_YMD"]) ? null : dataReader["FUTAI_SHOKAI_SHODAN_YMD"].ToString();
                    lstResponseDTO.Add(responseDTO);
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
            return lstResponseDTO;
        }
        /// <summary>
        /// Get ken name from ken cd
        /// </summary>
        /// <param name="ken_CD"></param>
        /// <returns></returns>
        private string GetKen_Name(int? ken_CD)
        {
            if (ken_CD==null)
            {
                return "";
            }
            #region Variables
            string query = @"SELECT KEN_NAME
                           FROM TM_KEN (NOLOCK)
                           WHERE KEN_CD='" + ken_CD + "'";

            string KenName ="" ;
            SqlConnection connection=null;
            #endregion
            try
            {
                connection = DBManager.Connection(Product3Const.CONSTRTAPS);
                connection.Open();
                 var temp_kenname = DBManager.ExecuteScalar(query, connection);
                 if (temp_kenname != null)
                 {
                     KenName = temp_kenname.ToString();
                 }
            }
            catch (SqlException ex)
            {
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
            catch
            {
                throw new SqlExceptionAPI(ItemIdCS.E999);
            }
            finally
            {
                connection.Close();
            }
            return KenName;
        }

        #endregion
    }
}
