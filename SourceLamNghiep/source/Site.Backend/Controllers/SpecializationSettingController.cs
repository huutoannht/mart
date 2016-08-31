using System.Web.Mvc;
using Data.DataContract;
using Models.Category;
using Share.Messages.BeScreens.CategoryRes;
using Site.Backend.Library.Attribute;

namespace Site.Backend.Controllers
{
    public class SpecializationSettingController : CategoryBaseController
    {
        public SpecializationSettingController()
            : base(BePage.SpecializationSetting)
        {
            CategoryType = CategoryType.Specialization;
            AddNewTitle = CategoryRes.AddNewSpecialization;
            EditTitle = CategoryRes.EditSpecialization;
        }

        [View]
        public ActionResult Index(CategoryIndexModel model)
        {
            PopulateIndexModel(model);

            return View(model);
        }
    }
}