using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_Test.DT
{
    public class ResponseDTO
    {
        /// <summary> 顧客番号 </summary>
        public string O_KOKYAKU_NO { get; set; }
        /// <summary> 店舗番号 </summary>
        public string O_TEMPO_CD { get; set; }
        /// <summary> 漢字姓 </summary>
        public string O_KANJI_SEI { get; set; }
        /// <summary> 漢字名 </summary>
        public string O_KANJI_MEI { get; set; }
        /// <summary> カナ姓 </summary>
        public string O_KANA_SEI { get; set; }
        /// <summary> カナ名 </summary>
        public string O_KANA_MEI { get; set; }
        /// <summary> 性別 </summary>
        public int? O_SEX { get; set; }
        /// <summary> 生年月日 </summary>
        public string O_SEINENGAPPI { get; set; }
        /// <summary> 郵便番号 </summary>
        public string O_ZIP { get; set; }
        /// <summary> 都道府県コード </summary>
        public int? O_TODOFUKEN_CD { get; set; }
        /// <summary> 住所1 </summary>
        public string O_JUSHO1 { get; set; }
        /// <summary> 住所2 </summary>
        public string O_JUSHO2 { get; set; }
        /// <summary> 電話番号 </summary>
        public string O_TEL { get; set; }
        /// <summary> 携帯番号 </summary>
        public string O_TEL_KEITAI { get; set; }
        /// <summary> FAX番号 </summary>
        public string O_FAX { get; set; }
        /// <summary> メースアドレス </summary>
        public string O_MAIL { get; set; }
        /// <summary> 携帯アドレス </summary>
        public string O_MAIL_KEITAI { get; set; }


        /// <summary> 販売店舗コード </summary>
        public string O_HAMBAI_TEMPO_CD { get; set; }
        /// <summary> 販売商談管理番号 </summary>
        public string O_HAMBAI_SHODAN_KANRI_NO { get; set; }
        /// <summary> 販売商談管理枝番号 </summary>
        public int? O_HAMBAI_SHODAN_KANRI_EDA_NO { get; set; }
        /// <summary> 販売商談種別 </summary>
        public string O_HAMBAI_SHODAN_SHUBETSU { get; set; }
        /// <summary> 販売商談分類 </summary>
        public string O_HAMBAI_SHODAN_BUNRUI { get; set; }
        /// <summary> 販売初回商談日 </summary>
        public string O_HAMBAI_SHOKAI_SHODAN_YMD { get; set; }
        /// <summary> 買取店舗コード </summary>
        public string O_KAITORI_TEMPO_CD { get; set; }
        /// <summary> 買取商談管理番号 </summary>
        public string O_KAITORI_SHODAN_KANRI_NO { get; set; }
        /// <summary> 買取商談管理枝番号 </summary>
        public int? O_KAITORI_SHODAN_KANRI_EDA_NO { get; set; }
        /// <summary> 買取商談種別 </summary>
        public string O_KAITORI_SHODAN_SHUBETSU { get; set; }
        /// <summary> 買取商談分類 </summary>
        public string O_KAITORI_SHODAN_BUNRUI { get; set; }
        /// <summary> 買取初回商談日 </summary>
        public string O_KAITORI_SHOKAI_SHODAN_YMD { get; set; }

        /// <summary> 付帯店舗コード </summary>
        public string O_FUTAI_TEMPO_CD { get; set; }
        /// <summary> 付帯商談管理番号 </summary>
        public string O_FUTAI_SHODAN_KANRI_NO { get; set; }
        /// <summary> 付帯商談管理枝番号 </summary>
        public int? O_FUTAI_SHODAN_KANRI_EDA_NO { get; set; }
        /// <summary> 付帯商談種別 </summary>
        public string O_FUTAI_SHODAN_SHUBETSU { get; set; }
        /// <summary> 付帯商談分類 </summary>
        public string O_FUTAI_SHODAN_BUNRUI { get; set; }
        /// <summary> 付帯初回商談日 </summary>
        public string O_FUTAI_SHOKAI_SHODAN_YMD { get; set; }


    }
}
