using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Data.DataContract;
using Data.DataContract.ProductDC;
using Share.Helper.Cache;
using Share.Helper.Models;
using ML.Common;
using ML.Common.Extensions.Linq;
using StructureMap;

namespace Share.Helper
{
    public static partial class SiteUtils
    {
        public static IDictionary<string, object> JsonObjectMergeProperties(bool success, string message, object data)
        {
            dynamic expando = new ExpandoObject();
            expando.success = success;
            expando.message = message;

            if (data == null)
                return expando;//new ExpandoObject();

            var result = expando as IDictionary<string, object>;
            foreach (System.Reflection.PropertyInfo fi in data.GetType().GetProperties())
            {
                result[fi.Name] = fi.GetValue(data, null);
            }

            return result;
        }

        public static bool IsPostAction(this HttpRequestBase httpRequest)
        {
            return httpRequest.HttpMethod.ToUpper() == "POST";
        }

        public static List<TextValueModel> GetTimeZones()
        {
            var list = new List<TimeZoneModel>();

            var dateTimeHelper = ObjectFactory.GetInstance<IDateTimeHelper>();

            var timeZones = dateTimeHelper.GetTimeZones();

            timeZones.ForEach(tz => list.Add(new TimeZoneModel
            {
                Id = tz.Id,
                DisplayName = tz.DisplayName,
                Name = tz.DisplayName.Split(new[] { ")" }, StringSplitOptions.RemoveEmptyEntries)[1]
            }));

            var results = new List<TextValueModel>();

            list.OrderBy(l => l.Name).ForEach(l => results.Add(new TextValueModel
            {
                Text = l.DisplayName,
                Value = l.Id
            }));

            return results;
        }

        public static bool TimeZoneIsValid(string timeZone)
        {
            return GetTimeZones().Any(i => i.Value == timeZone);
        }

        public static string GetCardNumberMask(string cardNumber)
        {
            cardNumber = cardNumber.ToStr();

            if (string.IsNullOrWhiteSpace(cardNumber)) return string.Empty;

            var sb = new StringBuilder();
            sb.Append("".PadLeft(12, '*'));
            if (cardNumber.Length > 4)
            {
                sb.Append(cardNumber.Substring(cardNumber.Length - 4));
            }

            return sb.ToString();
        }

        public static string GetCardCVSMask(string cvs)
        {
            if (string.IsNullOrWhiteSpace(cvs)) return string.Empty;

            cvs = cvs.ToStr();
            var sb = new StringBuilder();
            sb.Append("".PadLeft(2, '*'));
            if (cvs.Length > 1)
            {
                sb.Append(cvs.Substring(cvs.Length - 1));
            }

            return sb.ToString();
        }

        public static double KmsToMiles(double kms)
        {
            return kms * 0.621371;
        }

        public static double MetersToMiles(double meters)
        {
            return KmsToMiles(MetersToKms(meters));
        }

        public static double MetersToKms(double meters)
        {
            return meters / 1000;
        }

        public static double MilesToKms(double miles)
        {
            return miles / 0.621371;
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static string GeAllowedImageFileExtensions()
        {
            return "['png', 'gif','jpg','jpeg', 'ico', 'bmp']";
        }

        public static bool IsImageFile(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToStr().ToLower();

            return extension.EndsWith("png")
                   || extension.EndsWith("gif")
                   || extension.EndsWith("jpg")
                   || extension.EndsWith("jpeg")
                   || extension.EndsWith("ico")
                   || extension.EndsWith("bmp");
        }

        public static bool IsExcelFile(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToStr().ToLower();

            return extension.EndsWith("xls")
                   || extension.EndsWith("csv");
        }

        public static bool IsDangerousFile(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToStr().ToLower();

            return extension.EndsWith("exe")
                   || extension.EndsWith("com")
                   || extension.EndsWith("bat")
                   || extension.EndsWith("bin")
                   || extension.EndsWith("cmd")
                   || extension.EndsWith("gadget")
                   || extension.EndsWith("inf")
                   || extension.EndsWith("ins")
                   || extension.EndsWith("inx")
                   || extension.EndsWith("isu")
                   || extension.EndsWith("job")
                   || extension.EndsWith("jse")
                   || extension.EndsWith("lnk")
                   || extension.EndsWith("msc")
                   || extension.EndsWith("msi")
                   || extension.EndsWith("msp")
                   || extension.EndsWith("mst")
                   || extension.EndsWith("paf")
                   || extension.EndsWith("pif")
                   || extension.EndsWith("ps1")
                   || extension.EndsWith("reg")
                   || extension.EndsWith("rsg")
                   || extension.EndsWith("sct")
                   || extension.EndsWith("shb")
                   || extension.EndsWith("shs")
                   || extension.EndsWith("u3p")
                   || extension.EndsWith("vb")
                   || extension.EndsWith("vbe")
                   || extension.EndsWith("vbs")
                   || extension.EndsWith("vbscript")
                   || extension.EndsWith("ws")
                   || extension.EndsWith("wsf")
                   || extension.EndsWith("cpl");
        }

        public static bool ImageSizeIsValid(long length)
        {
            return (length / 1048576) <= 5;
        }

        public static int GetTotalPage(decimal totalRows, decimal pageSize)
        {
            var val = (totalRows / pageSize).ToString();

            if (val.Contains("."))
            {
                var arr = val.Split('.').ToList();
                var x = arr[0].ToInt();
                var y = arr[1].ToInt();

                if (x == 0) return 1;
                if (y == 0) return x;
                return x + 1;
            }

            return val.ToInt();
        }

        public static void CreateThumbImage(string psSourcePath, string psDestinationPath, int iThumbWidth = 300)
        {
            try
            {
                if (!File.Exists(psSourcePath))
                {
                    return;
                }

                if (psSourcePath.ToLower().EndsWith("ico"))
                {
                    File.Copy(psSourcePath, psDestinationPath);
                    return;
                }

                using (var oImage = FromFile(psSourcePath))
                {
                    var iThumbHeight = iThumbWidth;

                    using (var oBmp = ResizeImage(oImage, iThumbWidth, iThumbHeight))
                    {
                        oBmp.Save(psDestinationPath);
                    }
                }
            }
            catch (OutOfMemoryException ex)
            {
                if (File.Exists(psSourcePath))
                {
                    File.Delete(psSourcePath);
                }
            }
        }

        public static Image FromFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var result = Image.FromStream(fs);
                fs.Close();
                // We MUST call the constructor here, 
                // otherwise the bitmap will still be linked to the original file 
                return new Bitmap(result);
            }
        } 

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static bool EnumIsUnique(List<Type> types, bool detect = false)
        {
            var list = new List<short>();
            types.ForEach(type =>
            {
                var arr = Enum.GetValues(type);
                list.AddRange(arr.Cast<short>());
            });

            if (detect)
            {
                foreach (var value in list)
                {
                    if (list.Count(i => i == value) != 1)
                    {
                        continue;
                    }
                }
            }

            return list.Count == list.Distinct().Count();
        }

    }
}
