using System;
using System.Collections.Generic;
using System.Linq;
using ML.Utils.SMS;
using Data.DataContract;
using Data.DataContract.AppSettingDC;
using Share.Helper.Cache;
using StructureMap;

namespace Share.Helper
{
    public class SmsUtils
    {
        #region Singleton

        private readonly static Lazy<SmsUtils> LazyInstance = new Lazy<SmsUtils>(() => new SmsUtils());

        public static SmsUtils Instance
        {
            get { return LazyInstance.Value; }
        }

        #endregion

        private readonly ISmsManager _smsManager;

        public SmsUtils()
        {
            _smsManager = ObjectFactory.GetInstance<ISmsManager>();
        }

        public void SendSms(string to, string content, Dictionary<string, string> mapKeyValues)
        {
            var appSetting =
                ObjectFactory.GetInstance<ICacheHelper>().GetAppSettings(AppSettingType.SmsVendorSetting);

            var vendor =
                (appSetting.FirstOrDefault(i => i.Name == ConstKeys.SmsVendor) ?? new AppSetting()).Value;

            if (string.IsNullOrWhiteSpace(vendor) || vendor == "Twilio")
            {
                var smsSetting = new SmsTwilioSetting
                {
                    ApiVersion = "2010-04-01",
                    ApiUrl = "https://api.twilio.com",
                    AccountSid =
                        (appSetting.FirstOrDefault(i => i.Name == ConstKeys.TwilioAccountSid) ?? new AppSetting()).Value,
                    AuthToken =
                        (appSetting.FirstOrDefault(i => i.Name == ConstKeys.TwilioAuthToken) ?? new AppSetting()).Value,
                    FromPhone =
                        (appSetting.FirstOrDefault(i => i.Name == ConstKeys.TwilioFromPhone) ?? new AppSetting()).Value
                };

                _smsManager.SendSms(SmsProvider.Twilio, smsSetting, to, SiteUtils.MapContent(content, mapKeyValues));
            }
            else if (vendor == "Tropo")
            {

            }
            else if (vendor == "Plivo")
            {
                
            }
        }
    }
}
