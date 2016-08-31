using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Data.DataContract;
using Data.DataContract.SystemEmailTemplateDC;
using Share.Helper.Cache;
using ML.Common;
using ML.Utils.Email;
using StructureMap;

namespace Share.Helper
{
    public sealed class EmailUtils
    {
        #region Singleton

        private readonly static Lazy<EmailUtils> LazyInstance = new Lazy<EmailUtils>(() => new EmailUtils());

        public static EmailUtils Instance
        {
            get { return LazyInstance.Value; }
        }

        #endregion

        private readonly IEmailHelper _emailHelper;
        private readonly ICacheHelper _cacheHelper;

        public EmailUtils()
        {
            _emailHelper = ObjectFactory.GetInstance<IEmailHelper>();
            _cacheHelper = ObjectFactory.GetInstance<ICacheHelper>();
        }

        public List<string> GetValidEmails(List<string> emails)
        {
            var pattern = std.AppSettings["ValidatorEmailRegex"] ?? @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            return emails.Where(e => !string.IsNullOrWhiteSpace(e) && Regex.IsMatch(e, pattern)).Select(e => e.ToLower()).Distinct().ToList();
        }

        public void SendEmail(string to, SystemEmailTemplate emailTemplate, Dictionary<string, string> mapKeyValues, SmtpSetting smtpSetting = null)
        {
            SendEmail(to, emailTemplate.Subject, emailTemplate.Content, mapKeyValues, smtpSetting);
        }

        public void SendEmail(string to, string subject, string content, Dictionary<string, string> mapKeyValues, SmtpSetting smtpSetting = null)
        {
            var mail = new MailMessage
            {
                IsBodyHtml = true,
                Subject = SiteUtils.MapContent(subject, mapKeyValues),
                Body = SiteUtils.MapContent(content, mapKeyValues)
            };

            mail.To.Add(new MailAddress(to));

            SendEmail(mail, smtpSetting);
        }

        private SmtpSetting _currentSmtp;

        public void SendEmail(MailMessage mail, SmtpSetting smtpSetting = null)
        {
            _currentSmtp = GetSmtpSetting();

            _emailHelper.SendEmail(mail, _currentSmtp);
        }

        public SmtpSetting GetSmtpSetting()
        {
            var smtpDb = _cacheHelper.GetAppSettings(AppSettingType.Smtp);

            if (smtpDb.Count == 0)
            {
                return null;
            }

            var smtpUserName = smtpDb.FirstOrDefault(x => x.Name == "SmtpUserName");
            var smtpFullName = smtpDb.FirstOrDefault(x => x.Name == "SmtpFullName");
            var smtpPort = smtpDb.FirstOrDefault(x => x.Name == "SmtpPort");
            var smtpPassword = smtpDb.FirstOrDefault(x => x.Name == "SmtpPassword");
            var smtpServer = smtpDb.FirstOrDefault(x => x.Name == "SmtpServer");
            var smtpEnableSsl = smtpDb.FirstOrDefault(x => x.Name == "SmtpEnableSsl");
            var smtpSubAccount = smtpDb.FirstOrDefault(x => x.Name == "SmtpSubAccount");
            var smtpFrom = smtpDb.FirstOrDefault(x => x.Name == "SmtpFrom");
            var smtpSendingDomain = smtpDb.FirstOrDefault(x => x.Name == "SmtpSendingDomain");

            return new SmtpSetting
            {
                EnableSsl = smtpEnableSsl != null && smtpEnableSsl.Value.ToBool(),
                From = smtpFrom != null ? smtpFrom.Value : string.Empty,
                FullName = smtpFullName != null ? smtpFullName.Value : string.Empty,
                Password = smtpPassword != null ? smtpPassword.Value : string.Empty,
                Port = smtpPort != null ? smtpPort.Value.ToInt() : 0,
                Server = smtpServer != null ? smtpServer.Value : string.Empty,
                SubAccount = smtpSubAccount != null ? smtpSubAccount.Value : string.Empty,
                UserName = smtpUserName != null ? smtpUserName.Value : string.Empty,
                ViaDomain = smtpSendingDomain != null ? smtpSendingDomain.Value : string.Empty
            };
        }
    }
}
