using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Data.DataContract;
using Share.Helper.Models;

namespace Share.Helper
{
    public class CreditCardUtility
    {
        public static bool IsValidNumber(string cardNum)
        {
            return GetCardTypeFromNumber(cardNum) != CreditCardType.Invalid
                && GetCardTypeFromNumber(cardNum) != CreditCardType.Unknown;
        }

        public static CreditCardType GetCardTypeFromNumber(string cardNum)
        {
            if (string.IsNullOrWhiteSpace(cardNum)) return CreditCardType.Unknown;

            var arr = new List<TextValueModel>
            {
                new TextValueModel{Text = CreditCardType.Visa.ToString(), Value = "^4[0-9]{12}(?:[0-9]{3})?$"},
                new TextValueModel{Text = CreditCardType.MasterCard.ToString(), Value = "^5[1-5][0-9]{14}$"},
                new TextValueModel{Text = CreditCardType.Discover.ToString(), Value = "^6(?:011|5[0-9]{2})[0-9]{12}$"},
                new TextValueModel{Text = CreditCardType.AmericanExpress.ToString(), Value = "^3[47][0-9]{13}$"},
                new TextValueModel{Text = CreditCardType.JCB.ToString(), Value = @"^(?:2131|1800|35\d{3})\d{11}$"},
                new TextValueModel{Text = CreditCardType.DinersClub.ToString(), Value = "^3(?:0[0-5]|[68][0-9])[0-9]{11}$"},
                new TextValueModel{Text = CreditCardType.Dankort.ToString(), Value = @"^(5019)\d+$"},
                new TextValueModel{Text = CreditCardType.Electron.ToString(), Value = @"^(4026|417500|4405|4508|4844|4913|4917)\d+$"},
                new TextValueModel{Text = CreditCardType.Maestro.ToString(), Value = @"^(5018|5020|5038|5612|5893|6304|6759|6761|6762|6763|0604|6390)\d+$"},
                new TextValueModel{Text = CreditCardType.Interpayment.ToString(), Value = @"^(636)\d+$"},
                new TextValueModel{Text = CreditCardType.Unionpay.ToString(), Value = @"^(62|88)\d+$"},
            };

            var matchItem = arr.FirstOrDefault(item => (new Regex(item.Value)).IsMatch(cardNum));

            if (matchItem != null)
            {
                return (CreditCardType)Enum.Parse(typeof(CreditCardType), matchItem.Text, true);
            }

            if (cardNum.Substring(0, 2) == "63") return CreditCardType.Paymentech;
            if (cardNum.Substring(0, 2) == "56") return CreditCardType.AustralianBankCard;

            return CreditCardType.Invalid;
        }

        public static bool PassesLuhn(string cardNumber)
        {
            //Clean the card number- remove dashes and spaces
            cardNumber = cardNumber.Replace("-", "").Replace(" ", "");

            //Convert card number into digits array
            int[] digits = new int[cardNumber.Length];
            for (int len = 0; len < cardNumber.Length; len++)
            {
                digits[len] = Int32.Parse(cardNumber.Substring(len, 1));
            }

            //Luhn Algorithm
            //Adapted from code availabe on Wikipedia at
            //http://en.wikipedia.org/wiki/Luhn_algorithm
            int sum = 0;
            bool alt = false;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int curDigit = digits[i];
                if (alt)
                {
                    curDigit *= 2;
                    if (curDigit > 9)
                    {
                        curDigit -= 9;
                    }
                }
                sum += curDigit;
                alt = !alt;
            }

            //If Mod 10 equals 0, the number is good and this will return true
            return sum % 10 == 0;
        }
    }
}
