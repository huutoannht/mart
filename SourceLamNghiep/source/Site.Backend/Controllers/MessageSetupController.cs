using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.HtmlContent;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;
using Data.DataContract;
using Data.DataContract.HtmlContentDC;
using Share.Helper;
using Share.Messages;
using Share.Messages.BeScreens.MessageSetup;
using ML.Common;

namespace Site.Backend.Controllers
{
    public class MessageSetupController : AuthorisedController
    {
        public MessageSetupController() : base(BePage.MessageSetup) { }

        [View]
        public ActionResult Index(HtmlContentModel model)
        {
            ModelState.Clear();

            if (!string.IsNullOrWhiteSpace(model.LanguageCode))
            {
                var type = model.Type;
                var languageCode = model.LanguageCode;
                model = ServiceHelper.HtmlContent.ExecuteDispose(s => s.GetHtmlContent(type, HtmlContentDisplayType.WebSite, languageCode))
                    .Map<HtmlContent, HtmlContentModel>()
                    ?? new HtmlContentModel
                    {
                        Type = type,
                        LanguageCode = languageCode,
                        HtmlContentDisplayType = HtmlContentDisplayType.WebSite
                    };
            }

            if (!string.IsNullOrWhiteSpace(model.LanguageCode) && !SiteUtils.LanguageCodeIsValid(model.LanguageCode))
            {
                SendNotification(NotifyType.Error, MessageSetup.LanguageCodeIsInvalid);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Update]
        [HttpPost]
        public ActionResult Save(HtmlContentModel model)
        {
            if (ModelState.IsValid)
            {
                HtmlContent entity;
                if (model.IsNew)
                {
                    entity = model.Map<HtmlContentModel, HtmlContent>();
                    entity.InitId();
                }
                else
                {
                    entity = ServiceHelper.HtmlContent.ExecuteDispose(s => s.GetHtmlContent(model.Type, HtmlContentDisplayType.WebSite, model.LanguageCode));
                    if (entity == null)
                    {
                        SendNotification(NotifyType.Error, BackendMessage.CannotLoadData);
                        goto end;
                    }

                    entity.Content = model.Content;
                }

                var res = ServiceHelper.HtmlContent.ExecuteDispose(s => s.SaveHtmlContent(new SaveRequest
                {
                    Entity = entity
                }));

                if (res.Success)
                {
                    CacheHelper.ClearGetHtmlContents();
                    SendNotification(NotifyType.Success, BackendMessage.SaveDataSuccess);
                    goto end;
                }

                SendNotification(NotifyType.Error, BackendMessage.ErrorOccure);
                goto end;
            }

            SendNotification(NotifyType.Error, GetModelStateErrors());
        end:
            return RedirectToAction("Index", new { model.Type, model.LanguageCode });
        }
    }
}