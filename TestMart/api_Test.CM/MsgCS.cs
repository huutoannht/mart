using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace api_Test.CM
{
    public class MsgCS
    {
        /// <summary>タイムアウトエラー</summary>
        public const string E001 = "タイムアウトエラー";
        /// <summary>DBへの接続失敗</summary>
        public const string E002 = "DB接続エラー";
        /// <summary>実行パラメータが不正</summary>
        public const string E003 = "パラメーターエラー";
        /// <summary>DBへのINSERT/UPDATE処理の失敗</summary>
        public const string E004 = "DB登録エラー";
        /// <summary>検知できない異常終了</summary>
        public const string E999 = "システムエラー";
    }
}
