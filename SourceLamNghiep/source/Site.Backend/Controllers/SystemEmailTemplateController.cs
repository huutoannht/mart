using System.Web.Mvc;
using Models.SystemEmailTemplate;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;
using Data.DataContract;
using Data.DataContract.SystemEmailTemplateDC;
using Share.Messages;
using ML.Common;

namespace Site.Backend.Controllers
{
    public class SystemEmailTemplateController : AuthorisedController
    {
        public SystemEmailTemplateController() : base(BePage.SystemEmailTemplates) { }

        [View]
        public ActionResult Index(SystemEmailTemplateIndexModel model)
        {
            ModelState.Clear();

            if (model.Type.HasValue && !string.IsNullOrWhiteSpace(model.Language))
            {
                model.SystemEmailTemplate = ServiceHelper.SystemEmailTemplate
                    .ExecuteDispose(s => s.GetSystemEmailTemplateByTypeAndLanguage(model.Type.Value, model.Language))
                    .Map<SystemEmailTemplate, SystemEmailTemplateModel>()
                    ?? new SystemEmailTemplateModel
                    {
                        Type = model.Type.Value,
                        Language = model.Language
                    };
            }

            return View(model);
        }

        [Update]
        public ActionResult Save(SystemEmailTemplateModel model)
        {
            if (ModelState.IsValid)
            {
                var emailTemplate = ServiceHelper.SystemEmailTemplate
                    .ExecuteDispose(s => s.GetSystemEmailTemplateByTypeAndLanguage(model.Type, model.Language))
                    ?? new SystemEmailTemplate
                    {
                        Type = model.Type,
                        Language = model.Language
                    };

                emailTemplate.Subject = model.Subject;
                emailTemplate.Content = model.Content;

                if (emailTemplate.IsNew)
                {
                    emailTemplate.InitId();
                }

                ServiceHelper.SystemEmailTemplate.ExecuteDispose(s => s.SaveSystemEmailTemplate(new SaveRequest
                {
                    Entity = emailTemplate
                }));

                CacheHelper.ClearGetSystemEmailTemplates();

                SendNotification(NotifyType.Success, BackendMessage.SaveDataSuccess);
                return RedirectToAction("Index", new { model.Type, model.Language });
            }

            SendNotification(NotifyType.Error, GetModelStateErrors());
            return RedirectToAction("Index", new { model.Type, model.Language });
        }
    }
}
