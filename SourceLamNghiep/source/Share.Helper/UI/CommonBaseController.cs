using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using Data.DataContract;
using ML.Common;
using Share.Helper.Extension;

namespace Share.Helper.UI
{
    public class CommonBaseController : Controller
    {
        protected string PhysicalDataImagesFolderPath
        {
            get
            {
                return Server.MapPath(ConstKeys.DataImageFolder);
            }
        }

        protected string PhysicalDataFilesFolderPath
        {
            get
            {
                return Server.MapPath(ConstKeys.DataFileFolder);
            }
        }

        protected string PhysicalDataFilePath(string fileName, DataChildFolder folder = DataChildFolder.Images)
        {
            fileName = Path.GetFileName(fileName).ToStr();

            switch (folder)
            {
                case DataChildFolder.Images:
                    return Server.MapPath(Path.Combine(ConstKeys.DataImageFolder, fileName));
                case DataChildFolder.Files:
                    return Server.MapPath(Path.Combine(ConstKeys.DataFileFolder, fileName));
            }

            return string.Empty;
        }

        protected void DeleteFile(string fileName)
        {
            fileName = Path.GetFileName(fileName).ToStr();

            var filePath = PhysicalDataFilePath(fileName, DataChildFolder.Files);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        protected void DeleteImageFile(string fileName)
        {
            fileName = Path.GetFileName(fileName).ToStr();

            if (Url.ImageIsExist(fileName))
            {
                System.IO.File.Delete(PhysicalDataFilePath(fileName));
            }

            if (Url.ImageIsExist(ConstKeys.ImageThumbPrefix + fileName))
            {
                System.IO.File.Delete(PhysicalDataFilePath(ConstKeys.ImageThumbPrefix + fileName));
            }

            if (Url.ImageIsExist(ConstKeys.ImageThumbSmallPrefix + fileName))
            {
                System.IO.File.Delete(PhysicalDataFilePath(ConstKeys.ImageThumbSmallPrefix + fileName));
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = Session[SessionKey.CurrentUserId.ToString()];
            if (Request.IsAuthenticated && (userId == null || (Guid)userId == Guid.Empty))
            {
                FormsAuthentication.SignOut();
            }
        }
    }
}
