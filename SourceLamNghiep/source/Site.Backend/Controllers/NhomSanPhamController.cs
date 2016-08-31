using System.Web.Mvc;
using Data.DataContract;
using Models.Category;
using Share.Messages.BeScreens.CategoryRes;
using Site.Backend.Library.Attribute;

namespace Site.Backend.Controllers
{
    public class NhomSanPhamController : CategoryBaseController
    {
        public NhomSanPhamController() : base(BePage.NhomSanPham)
        {
            CategoryType = CategoryType.Product;
            AddNewTitle = CategoryRes.AddNewCategory;
            EditTitle = CategoryRes.EditCategory;
        }

        [View]
        public ActionResult Index(CategoryIndexModel model)
        {
            PopulateIndexModel(model);

            return View(model);
        }
    }
}