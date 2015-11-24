using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_Test.DT
{
    public class RequestDTO
    {
        /// <summary>  店舗番号 </summary>
        public string I_TEMPO_CD { get; set; }
        /// <summary>  漢字姓 </summary>
        public string I_KANJI_SEI { get; set; }
        /// <summary>  漢字名 </summary>
        public string I_KANJI_MEI { get; set; }
        /// <summary>  カナ姓 </summary>
        public string I_KANA_SEI { get; set; }
        /// <summary>  カナ名 </summary>
        public string I_KANA_MEI { get; set; }
        ///// <summary>  性別 </summary>
        //public int? I_SEX { get; set; }
        /// <summary>  生年月日 </summary>
        public string I_SEINENGAPPI { get; set; }
        ///// <summary>  郵便番号 </summary>
        //public string I_ZIP { get; set; }
        /// <summary>  都道府県コード </summary>
        public int? I_TODOFUKEN_CD { get; set; }
        /// <summary>  住所1 </summary>
        public string I_JUSHO1 { get; set; }
        /// <summary>  住所2 </summary>
        public string I_JUSHO2 { get; set; }
        /// <summary>  電話番号 </summary>
        public string I_TEL { get; set; }
        /// <summary>  携帯番号 </summary>
        public string I_TEL_KEITAI { get; set; }
        ///// <summary>  FAX番号 </summary>
        //public string I_FAX { get; set; }
        /// <summary>  メースアドレス </summary>
        public string I_MAIL { get; set; }
        /// <summary>  携帯アドレス </summary>
        public string I_MAIL_KEITAI { get; set; }


        /// <summary>  CustomerNameKanji </summary>
        public string CustomerNameKanji
        {
            get {
                if (!string.IsNullOrEmpty(this.I_KANJI_SEI) || !string.IsNullOrEmpty(this.I_KANJI_MEI))
                {
                    return this.I_KANJI_SEI + this.I_KANJI_MEI;
                }
                return null;
            }
        }

        /// <summary>  CustomerNameKana </summary>
        public string CustomerNameKana
        {
            get
            {
                if (!string.IsNullOrEmpty(this.I_KANA_SEI) || !string.IsNullOrEmpty(this.I_KANA_MEI))
                {
                    return this.I_KANA_SEI + this.I_KANA_MEI;
                }	
                return null;
            }
        }

        /// <summary>  AddressAll </summary>
        public string AddressAll
        {
            get
            {
                if (!string.IsNullOrEmpty(this.I_JUSHO1) &&
                    !string.IsNullOrEmpty(this.I_JUSHO2))
                {
                    return this.I_JUSHO1.Trim()
                                              + this.I_JUSHO2.Trim();
                }
                return null;
            }
        }
    }
}
