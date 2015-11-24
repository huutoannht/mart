using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace api_Test.CM
{
    public class ItemIdCS
    {
        /// <summary>タイムアウトエラー</summary>
        public const string E001 = "E001";
        /// <summary>DB接続エラー</summary>
        public const string E002 = "E002";
        /// <summary>パラメーターエラー</summary>
        public const string E003 = "E003";
        /// <summary>指定した画像データは存在しません</summary>
        public const string E004 = "E004";
        /// <summary>システムエラー</summary>
        public const string E999 = "E999";
        /// <summary>INPUT : 差分モード</summary>
        public const string I_LINK_CD_UPDATE_DATE = "001";
        /// <summary>INPUT : 出品登録番号モード</summary>
        public const string I_LINK_CD_SHUPPINN = "002";
        /// <summary>INPUT : QS_CODEモード</summary>
        public const string I_LINK_CD_QS_CODE = "003";
        /// <summary>OUTPUT : 正常終了</summary>
        public const string O_RETURN_CD_SUCCESS = "001";
        /// <summary>OUTPUT : 異常終了</summary>
        public const string O_RETURN_CD_FAIL = "999";
    }
}
