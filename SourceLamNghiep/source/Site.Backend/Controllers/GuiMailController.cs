using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Data.DataContract;
using ML.Common;
using ML.Common.Extensions.Linq;
using Share.Helper;
using Share.Messages.BeScreens.GuiMail;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;
using Site.Backend.Models;

namespace Site.Backend.Controllers
{
    public class GuiMailController : AuthorisedController
    {
        public GuiMailController() : base(BePage.GuiMail) { }

        [View]
        public ActionResult Index()
        {
            var model = new ComposeMailModel();
            return View(model);
        }

        [Update]
        public ActionResult Send(ComposeMailModel model)
        {
            if (ModelState.IsValid)
            {
                var emails = GetEmails(model);

                if (!emails.Any())
                {
                    return JsonObject(false, GuiMail.ThereNoValidEmail);
                }

                var mail = new MailMessage();
                mail.To.Add(emails.First());
                if (emails.Count > 1)
                {
                    emails.Where(i=>i != emails.First()).ForEach(mail.Bcc.Add);
                }

                mail.Subject = model.Subject;
                mail.Body = model.Content;
                mail.IsBodyHtml = true;

                for (var i = 0; i < Request.Files.Count; ++i)
                {
                    var file = Request.Files[i];
                    if (file != null &&
                        !string.IsNullOrWhiteSpace(file.FileName))
                    {
                        var attachment = new Attachment(file.InputStream, file.FileName, file.ContentType);
                        mail.Attachments.Add(attachment);
                    }
                }

                EmailUtils.Instance.SendEmail(mail);

                return JsonObject(true, GuiMail.SendEmailSuccessful);
            }

            return JsonObject(false, GetModelStateErrors());
        }

        #region private

        private List<string> GetEmails(ComposeMailModel model)
        {
            var list = new List<string>();

            if (model.SendToAllRegisteredCustomer)
            {
                list.AddRange(ServiceHelper.Customer.ExecuteDispose(s => s.GetAllEmails()));
            }

            var arr = model.Emails.ToStr().Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            list.AddRange(arr);

            list = list.Select(s => s.ToLower()).Distinct().Where(std.IsEmail).ToList();

            return list;
        }

        #endregion
    }
}