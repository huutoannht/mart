using System.Web.Mvc;
using Data.DataContract;
using Models.Category;
using Share.Messages.BeScreens.CategoryRes;
using Site.Backend.Library.Attribute;

namespace Site.Backend.Controllers
{
    public class UOMController : CategoryBaseController
    {
        public UOMController()
            : base(BePage.UOM)
        {
            AddNewTitle = CategoryRes.AddNewUOM;
            EditTitle = CategoryRes.EditUOM;

            CategoryType = CategoryType.UOM;
        }

        [View]
        public ActionResult Index(CategoryIndexModel model)
        {
            PopulateIndexModel(model);

            return View(model);
        }
    }
}