using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Data.DataContract;
using Data.DataContract.CustomerDC;
using Data.DataContract.DataLogDC;
using ExcelUtil.Spreadsheet;
using ML.Common;
using Models.Customer;
using Models.DataLog;
using Share.Helper;
using Share.Helper.Extension;
using Share.Messages;
using Share.Messages.BeScreens.CustomerRes;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;
using WebGrease.Css.Extensions;
using FindRequest = Data.DataContract.CustomerDC.FindRequest;
using SaveRequest = Data.DataContract.CustomerDC.SaveRequest;

namespace Site.Backend.Controllers
{
    public class CustomerBaseController : AuthorisedController
    {
        protected readonly CustomerType CustomerType;
        private readonly string editViewName = "Edit";
        private readonly string editImageViewName = "EditImage";

        protected string AddNewTitle { get; set; }
        protected string EditTitle { get; set; }

        public CustomerBaseController(BePage bePage, CustomerType customerType) : base(bePage)
        {
            CustomerType = customerType;
        }

        #region customer

        public ActionResult GetListCustomer(CustomerIndexModel model)
        {
            PopulateIndexModel(model);
            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Customer/_list", model)
            });
        }

        [View]
        public ActionResult Add()
        {
            ViewBag.AddNewTitle = AddNewTitle;
            var model = new CustomerModel
            {
                CustomerType = CustomerType
            };

            PopulateVisitIndexModel(model);
            PopulateDataLogIndexModel(model);

            return View(editViewName, model);
        }

        [View]
        public ActionResult Edit(Guid id)
        {
            ViewBag.EditTitle = EditTitle;

            var customer = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(id));

            if (customer == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            var model = customer.Map<Customer, CustomerModel>();

            model.SundayStartStr = model.SundayStart.ToTimeString();
            model.SundayEndStr = model.SundayEnd.ToTimeString();

            model.MondayStartStr = model.MondayStart.ToTimeString();
            model.MondayEndStr = model.MondayEnd.ToTimeString();

            model.TuesdayStartStr = model.TuesdayStart.ToTimeString();
            model.TuesdayEndStr = model.TuesdayEnd.ToTimeString();

            model.WednesdayStartStr = model.WednesdayStart.ToTimeString();
            model.WednesdayEndStr = model.WednesdayEnd.ToTimeString();

            model.ThursdayStartStr = model.ThursdayStart.ToTimeString();
            model.ThursdayEndStr = model.ThursdayEnd.ToTimeString();

            model.FridayStartStr = model.FridayStart.ToTimeString();
            model.FridayEndStr = model.FridayEnd.ToTimeString();

            model.SaturdayStartStr = model.SaturdayStart.ToTimeString();
            model.SaturdayEndStr = model.SaturdayEnd.ToTimeString();

            PopulateVisitIndexModel(model);
            PopulateDataLogIndexModel(model);

            return View(editViewName, model);
        }

        public ActionResult GetDropDownDistrictsWards(CustomerModel model)
        {
            return JsonObject(true, string.Empty, new
            {
                dropDownDistrict = PartialViewToString("BaseView/Customer/Edit/_dropDownDistrict", model)
            });
        }

        public ActionResult GetDropDownDistrictsForFilterIndex(CustomerIndexModel model)
        {
            return JsonObject(true, string.Empty, new
            {
                dropDownDistrict = PartialViewToString("BaseView/Customer/_dropDownDistrict", model)
            });
        }

        public ActionResult SaveCustomer(CustomerModel model)
        {
            if (model.IsNew && !CanAdd)
            {
                return GetAddDeniedResult();
            }

            if (!model.IsNew && !CanUpdate)
            {
                return GetUpdateDeniedResult();
            }

            ModelState.Remove("City");
            ModelState.Remove("District");
            if (!ModelState.IsValid && GetModelStateErrorList().Any())
            {
                return JsonObject(false, GetModelStateErrors());
            }

            #region fill working hours

            DateTime dateTime;

            if (!string.IsNullOrWhiteSpace(model.SundayStartStr))
            {
                dateTime = DateTime.Parse(model.SundayStartStr, CultureInfo.CurrentCulture);
                model.SundayStart = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            if (!string.IsNullOrWhiteSpace(model.SundayEndStr))
            {
                dateTime = DateTime.Parse(model.SundayEndStr, CultureInfo.CurrentCulture);
                model.SundayEnd = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            //---

            if (!string.IsNullOrWhiteSpace(model.MondayStartStr))
            {
                dateTime = DateTime.Parse(model.MondayStartStr, CultureInfo.CurrentCulture);
                model.MondayStart = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            if (!string.IsNullOrWhiteSpace(model.MondayEndStr))
            {
                dateTime = DateTime.Parse(model.MondayEndStr, CultureInfo.CurrentCulture);
                model.MondayEnd = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            //--

            if (!string.IsNullOrWhiteSpace(model.TuesdayStartStr))
            {
                dateTime = DateTime.Parse(model.TuesdayStartStr, CultureInfo.CurrentCulture);
                model.TuesdayStart = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            if (!string.IsNullOrWhiteSpace(model.TuesdayEndStr))
            {
                dateTime = DateTime.Parse(model.TuesdayEndStr, CultureInfo.CurrentCulture);
                model.TuesdayEnd = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            //---

            if (!string.IsNullOrWhiteSpace(model.WednesdayStartStr))
            {
                dateTime = DateTime.Parse(model.WednesdayStartStr, CultureInfo.CurrentCulture);
                model.WednesdayStart = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            if (!string.IsNullOrWhiteSpace(model.WednesdayEndStr))
            {
                dateTime = DateTime.Parse(model.WednesdayEndStr, CultureInfo.CurrentCulture);
                model.WednesdayEnd = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            //---

            if (!string.IsNullOrWhiteSpace(model.ThursdayStartStr))
            {
                dateTime = DateTime.Parse(model.ThursdayStartStr, CultureInfo.CurrentCulture);
                model.ThursdayStart = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            if (!string.IsNullOrWhiteSpace(model.ThursdayEndStr))
            {
                dateTime = DateTime.Parse(model.ThursdayEndStr, CultureInfo.CurrentCulture);
                model.ThursdayEnd = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            //---

            if (!string.IsNullOrWhiteSpace(model.FridayStartStr))
            {
                dateTime = DateTime.Parse(model.FridayStartStr, CultureInfo.CurrentCulture);
                model.FridayStart = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            if (!string.IsNullOrWhiteSpace(model.FridayEndStr))
            {
                dateTime = DateTime.Parse(model.FridayEndStr, CultureInfo.CurrentCulture);
                model.FridayEnd = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            //---

            if (!string.IsNullOrWhiteSpace(model.SaturdayStartStr))
            {
                dateTime = DateTime.Parse(model.SaturdayStartStr, CultureInfo.CurrentCulture);
                model.SaturdayStart = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            if (!string.IsNullOrWhiteSpace(model.SaturdayEndStr))
            {
                dateTime = DateTime.Parse(model.SaturdayEndStr, CultureInfo.CurrentCulture);
                model.SaturdayEnd = new DateTime(ConstKeys.YearForTime, ConstKeys.MonthForTime, ConstKeys.DayForTime, dateTime.Hour, dateTime.Minute, 0);
            }

            #endregion

            #region visit

            var jsonSerializer = new JavaScriptSerializer();
            model.Visits = jsonSerializer.Deserialize<List<CustomerVisitModel>>(model.VisitJsonString);

            #endregion

            #region save list image

            var listImagePath = new List<string>();

            for (var i = 0; i < Request.Files.Count; ++i)
            {
                var file = Request.Files[i];
                if (file != null &&
                    !string.IsNullOrWhiteSpace(file.FileName))
                {
                    var extension = Path.GetExtension(file.FileName).ToStr();

                    if (!SiteUtils.IsImageFile(file.FileName))
                    {
                        return JsonObject(false, BackendMessage.FileTypeIsInvalid);
                    }

                    if (!SiteUtils.ImageSizeIsValid(file.ContentLength))
                    {
                        return JsonObject(false, BackendMessage.FileMaxSize5MB);
                    }

                    var imagePath = Guid.NewGuid() + extension;
                    listImagePath.Add(imagePath);

                    var filePath = PhysicalDataFilePath(imagePath);
                    file.SaveAs(filePath);
                }
            }

            #endregion

            var entity = model.Map<CustomerModel, Customer>();
            var oldData = new Customer();

            if (entity.IsNew)
            {
                entity.InitId();
                entity.CreatedDate = DateTime.UtcNow;
                entity.CreatedBy = CurrentUserId;
            }
            else
            {
                oldData = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(entity.Id));
                if (oldData == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }

                entity.CreatedDate = oldData.CreatedDate;
                entity.CreatedBy = oldData.CreatedBy;
            }

            entity.Visits.ForEach(i => i.CustomerId = entity.Id);

            var response = ServiceHelper.Customer.ExecuteDispose(s => s.SaveCustomer(new SaveRequest
            {
                Entity = entity
            }));

            if (response.Success)
            {
                if (listImagePath.Any())
                {
                    var listImages = new List<CustomerImage>();
                    listImagePath.ForEach(imagePath =>
                    {
                        var image = new CustomerImage
                        {
                            ImagePath = imagePath,
                            CustomerId = entity.Id,
                            CreatedDate = DateTime.UtcNow
                        };

                        image.InitId();

                        listImages.Add(image);
                    });

                    ServiceHelper.Customer.ExecuteDispose(s => s.SaveListImage(listImages));
                }

                SendNotification(NotifyType.Success, BackendMessage.SaveDataSuccess);

                #region Log

                var dataLog = new DataLog
                {
                    BeUserId = CurrentUserId,
                    Table = TableLog.Customer,
                    Action = model.IsNew ? ActionLog.Insert : ActionLog.Update,
                    LogDate = DateTime.UtcNow,
                    NewData = ObjectToLog(entity),
                    OldData = model.IsNew ? string.Empty : ObjectToLog(oldData),
                    ItemId = entity.Id
                };

                ServiceHelper.DataLog.ExecuteDispose(s => s.Insert(new Data.DataContract.DataLogDC.SaveRequest
                {
                    Entity = dataLog
                }));

                #endregion

                return JsonObject(true, string.Empty);
            }

            if (listImagePath.Any())
            {
                listImagePath.ForEach(DeleteImageFile);
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        [Delete]
        public ActionResult DeleteCustomer(Guid id)
        {
            var oldData = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(id));
            var response = ServiceHelper.Customer.ExecuteDispose(s => s.DeleteCustomer(id, PhysicalDataImagesFolderPath));

            if (response.Success)
            {
                #region Log

                var dataLog = new DataLog
                {
                    BeUserId = CurrentUserId,
                    Table = TableLog.Customer,
                    Action = ActionLog.Delete,
                    LogDate = DateTime.UtcNow,
                    OldData = ObjectToLog(oldData),
                    ItemId = id
                };

                ServiceHelper.DataLog.ExecuteDispose(s => s.Insert(new Data.DataContract.DataLogDC.SaveRequest
                {
                    Entity = dataLog
                }));

                #endregion

                return JsonObject(true, BackendMessage.DeleteSuccessfull);
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        [Delete]
        public ActionResult DeleteCustomers(string ids)
        {
            var arrId = ids.ToStr().Split(new[] { "#" }, StringSplitOptions.RemoveEmptyEntries).Select(i=>i.ToGuid()).ToList();
            if (!arrId.Any())
            {
                return JsonObject(false, BackendMessage.DataInvalid);
            }

            foreach (var id in arrId)
            {
                var customerId = id;
                var oldData = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(customerId));
                var response = ServiceHelper.Customer.ExecuteDispose(s => s.DeleteCustomer(customerId, PhysicalDataImagesFolderPath));
                if (!response.Success)
                {
                    return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
                }

                #region Log

                var dataLog = new DataLog
                {
                    BeUserId = CurrentUserId,
                    Table = TableLog.Customer,
                    Action = ActionLog.Delete,
                    LogDate = DateTime.UtcNow,
                    OldData = ObjectToLog(oldData),
                    ItemId = id
                };

                ServiceHelper.DataLog.ExecuteDispose(s => s.Insert(new Data.DataContract.DataLogDC.SaveRequest
                {
                    Entity = dataLog
                }));

                #endregion
            }

            return JsonObject(true, BackendMessage.DeleteSuccessfull);
        }

        public ActionResult GetChangeCustomerForm(Guid id)
        {
            var customer = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(id));
            if (customer == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Customer/Inc/_formChangeCustomerType", customer.Map<Customer, CustomerModel>())
            });
        }

        [Update]
        public ActionResult UpdateCustomerType(CustomerModel model)
        {
            var customer = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(model.Id));

            if (customer == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            var response = ServiceHelper.Customer.UpdateInfo(customer, new UpdateInfo
            {
                CustomerType = model.CustomerType
            });

            if (response.Success)
            {
                #region Log

                var newData = customer.Clone();
                newData.CustomerType = model.CustomerType;

                var dataLog = new DataLog
                {
                    BeUserId = CurrentUserId,
                    Table = TableLog.Customer,
                    Action = ActionLog.ChangeType,
                    LogDate = DateTime.UtcNow,
                    NewData = ObjectToLog(newData),
                    OldData = ObjectToLog(customer),
                    ItemId = customer.Id
                };

                ServiceHelper.DataLog.ExecuteDispose(s => s.Insert(new Data.DataContract.DataLogDC.SaveRequest
                {
                    Entity = dataLog
                }));

                #endregion

                return JsonObject(true, BackendMessage.SaveDataSuccess);
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        public ActionResult ViewCustomer(Guid id)
        {
            var customer = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(id));

            if (customer == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            var model = customer.Map<Customer, CustomerModel>();
            model.View = true;

            PopulateVisitIndexModel(model);
            PopulateDataLogIndexModel(model);

            return JsonObject(true, string.Empty, new
            {
                model,
                html = PartialViewToString("BaseView/Customer/_viewCustomer", model)
            });
        }

        public ActionResult GetDataLogs(CustomerModel model)
        {
            PopulateDataLogIndexModel(model);
            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Customer/Inc/_dataLog", model.DataLogIndex)
            });
        }

        public ActionResult GetCityNameDistrictName(short cityId, short districtId)
        {
            return JsonObject(true, string.Empty, new
            {
                cityName = SiteUtils.GetVietNamProvine(cityId).Name,
                districtName = SiteUtils.GetVietNamDistrict(districtId).Name
            });
        }

        #endregion

        #region visits

        public ActionResult GetVisits(CustomerModel model)
        {
            PopulateVisitIndexModel(model);
            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Customer/Edit/_visit", model.VisitIndex)
            });
        }

        public ActionResult EditVisit(CustomerModel model, Guid? id)
        {
            ModelState.Clear();
            var visitModel = new CustomerVisitModel
            {
                BeUserId = CurrentUserId,
                DateVisit=DateTime.Today

            };

            visitModel.InitId();

            if (id.HasValue)
            {
                visitModel = model.Visits.FirstOrDefault(i => i.Id == id.Value);
                if (visitModel == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }
            }

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Customer/Edit/_editVisit", visitModel)
            });
        }

        #endregion

        #region images

        [View]
        public ActionResult EditImages(Guid customerId)
        {
            var customer = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(customerId));

            if (customer == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }


            return View(editImageViewName, customer.Map<Customer, CustomerModel>());
        }

        public ActionResult GetAddNewImageHtml(Guid customerId)
        {
            var customer = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(customerId));

            if (customer == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Customer/ImageList/_addNewImage", customer.Map<Customer, CustomerModel>())
            });
        }

        [Update]
        public ActionResult SaveImage(Guid customerId)
        {
            var imagePath = string.Empty;

            #region save image

            if (Request.Files["fileImage"] != null && !string.IsNullOrWhiteSpace(Request.Files["fileImage"].FileName))
            {
                var file = Request.Files["fileImage"];

                var extension = Path.GetExtension(file.FileName).ToStr();

                if (!SiteUtils.IsImageFile(file.FileName))
                {
                    return JsonObject(false, BackendMessage.FileTypeIsInvalid);
                }

                if (!SiteUtils.ImageSizeIsValid(file.ContentLength))
                {
                    return JsonObject(false, BackendMessage.FileMaxSize5MB);
                }

                imagePath = Guid.NewGuid() + extension;

                var filePath = PhysicalDataFilePath(imagePath);
                file.SaveAs(filePath);
            }

            #endregion

            var oldData = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(customerId));

            if (oldData == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            var customerImage = new CustomerImage
            {
                ImagePath = imagePath,
                CustomerId = customerId,
                CreatedDate = DateTime.UtcNow
            };

            customerImage.InitId();

            var response = ServiceHelper.Customer.ExecuteDispose(s => s.SaveImage(customerImage));

            if (response.Success)
            {
                var customer = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(customerId));
                if (customer == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }

                #region Log

                var dataLog = new DataLog
                {
                    BeUserId = CurrentUserId,
                    Table = TableLog.Customer,
                    Action = ActionLog.InsertImage,
                    LogDate = DateTime.UtcNow,
                    NewData = ObjectToLog(customer),
                    OldData = ObjectToLog(oldData),
                    ItemId = customerId
                };

                ServiceHelper.DataLog.ExecuteDispose(s => s.Insert(new Data.DataContract.DataLogDC.SaveRequest
                {
                    Entity = dataLog
                }));

                #endregion

                return JsonObject(true, BackendMessage.SaveDataSuccess, new
                {
                    imageList = PartialViewToString("BaseView/Customer/ImageList/_imageList", customer.Map<Customer, CustomerModel>())
                });
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        [Update]
        public ActionResult DeleteImage(Guid id, Guid customerId)
        {
            var oldData = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(customerId));

            if (oldData == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            var response = ServiceHelper.Customer.ExecuteDispose(s => s.DeleteImage(id, PhysicalDataImagesFolderPath));

            if (response.Success)
            {
                var customer = ServiceHelper.Customer.ExecuteDispose(s => s.GetCustomer(customerId));

                if (customer == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }
                #region Log

                var dataLog = new DataLog
                {
                    BeUserId = CurrentUserId,
                    Table = TableLog.Customer,
                    Action = ActionLog.DeleteImage,
                    LogDate = DateTime.UtcNow,
                    NewData = ObjectToLog(customer),
                    OldData = ObjectToLog(oldData),
                    ItemId = customerId
                };

                ServiceHelper.DataLog.ExecuteDispose(s => s.Insert(new Data.DataContract.DataLogDC.SaveRequest
                {
                    Entity = dataLog
                }));

                #endregion

                return JsonObject(true, BackendMessage.DeleteSuccessfull, new
                {
                    imageList = PartialViewToString("BaseView/Customer/ImageList/_imageList", customer.Map<Customer, CustomerModel>())
                });
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        #endregion

        #region export excel

        [Export]
        public ActionResult ExportCustomers(CustomerIndexModel model)
        {
            model.IsExport = true;
            PopulateIndexModel(model);

            var name = CustomerType == CustomerType.Current
                ? CustomerType + " " + "Customers"
                : CustomerType.ToString();

            var ef = new ExcelFile();

            var ws = ef.Worksheets.Add(name);

            var ri = 0;
            var ci = 0;

            Action<ExcelCell> fontmatCaption = cell =>
            {
                cell.Style.Font.Weight = ExcelFont.BoldWeight;
                cell.Style.VerticalAlignment = VerticalAlignmentStyle.Top;
            };

            #region header

            ws.Cells[ri, ci].Value = "ClinicId";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "ClinicName";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "ClinicEmail";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "ClinicPhone";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "DentistName";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "DentistEmail";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "DentistPhone";
            fontmatCaption(ws.Cells[ri, ci]);

            #endregion

            #region body

            foreach (var item in model.Results)
            {
                ++ri;
                ci = 0;

                ws.Cells[ri, ci++].Value = item.ClinicId;
                ws.Cells[ri, ci++].Value = item.ClinicName;
                ws.Cells[ri, ci++].Value = item.ClinicEmail;
                ws.Cells[ri, ci++].Value = item.ClinicPhone;
                ws.Cells[ri, ci++].Value = item.DentistName;
                ws.Cells[ri, ci++].Value = item.DentistEmail;
                ws.Cells[ri, ci++].Value = item.DentistPhone;
            }

            #endregion

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = name + ".xls",

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            var memoryStream = new MemoryStream();

            ef.SaveXls(memoryStream);

            memoryStream.Position = 0;

            return File(memoryStream, "application/ms-excel");
        }

        #endregion

        #region import excel

        [Import]
        public ActionResult ImportCustomers()
        {
            var file = Request.Files["fileImport"];
            if (file == null || string.IsNullOrWhiteSpace(file.FileName))
            {
                return JsonObject(false, BackendMessage.DataInvalid);
            }

            var fileName = Path.GetFileName(file.FileName);

            var localFilePath = PhysicalDataFilePath(Guid.NewGuid() + Path.GetExtension(fileName), DataChildFolder.Files);

            if (!SiteUtils.IsExcelFile(fileName))
            {
                return JsonObject(false, BackendMessage.ExcelFileInvalid);
            }

            try
            {
                file.SaveAs(localFilePath);

                var table = LoadExcelFile(localFilePath);
                
                if (table == null || table.Rows.Count == 0)
                {
                    DeleteFile(localFilePath);
                    return JsonObject(false, BackendMessage.ThereIsNoDataRows);
                }

                if (table.Columns.Count != 7)
                {
                    DeleteFile(localFilePath);
                    return JsonObject(false, BackendMessage.ThereIsNotEnoughColumns);
                }

                //Check : must be have header row
                if (!CheckHaveHeaderRow(table))
                {
                    DeleteFile(localFilePath);
                    return JsonObject(false, BackendMessage.HeaderRowInvalid);
                }

                // Remove end empty row
                RemoveLastEmptyRow(table);

                //Check required field
                var listMessge = CheckInfoRequired(table);

                // Check email is valid format
                listMessge.AddRange(CheckEmailValidFormat(table));

                if (listMessge.Any())
                {
                    DeleteFile(localFilePath);
                    return JsonObject(false, string.Join("<br/>", listMessge));
                }

                //Save
                var listCustomer = new List<Customer>();
                PopulateSaveImportData(listCustomer, table);

                DeleteFile(localFilePath);

                var response = ServiceHelper.Customer.ExecuteDispose(s => s.SaveCustomers(listCustomer));

                if (response.Success)
                {
                    #region Log

                    listCustomer.ForEach(customer =>
                    {
                        var dataLog = new DataLog
                        {
                            BeUserId = CurrentUserId,
                            Table = TableLog.Customer,
                            Action = ActionLog.Insert,
                            LogDate = DateTime.UtcNow,
                            NewData = ObjectToLog(customer),
                            ItemId = customer.Id
                        };

                        ServiceHelper.DataLog.ExecuteDispose(s => s.Insert(new Data.DataContract.DataLogDC.SaveRequest
                        {
                            Entity = dataLog
                        }));
                    });

                    #endregion

                    return JsonObject(true, BackendMessage.ImportDataSuccessfull);
                }

                return JsonObject(false, GetMessageFromList(response.Messages));
            }
            catch (Exception e)
            {
                DeleteFile(localFilePath);
                return JsonObject(false, e.Message);
            }
        }

        public ActionResult GetImportForm()
        {
            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Customer/Inc/_formImport")
            });
        }

        private bool CheckHaveHeaderRow(DataTable table)
        {
            if (!string.Equals(table.Columns[0].ColumnName, "ClinicId", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[1].ColumnName, "ClinicName", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[2].ColumnName, "ClinicEmail", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[3].ColumnName, "ClinicPhone", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[4].ColumnName, "DentistName", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[5].ColumnName, "DentistEmail", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[6].ColumnName, "DentistPhone", StringComparison.CurrentCultureIgnoreCase)) return false;

            return true;
        }

        private void RemoveLastEmptyRow(DataTable table)
        {
            if (table.Rows.Count == 0) return;

            var listIndex = new List<int>();

            for (var i = table.Rows.Count - 1; i > 0; --i)
            {
                var lastRow = table.Rows[i];

                if (string.IsNullOrWhiteSpace(lastRow[0].ToStr()))
                {
                    listIndex.Add(i);
                }
                else
                {
                    break;
                }
            }

            listIndex.ForEach(i => table.Rows.RemoveAt(i));
        }

        private List<string> CheckInfoRequired(DataTable table)
        {
            var list = new List<string>();

            var index = 2;

            foreach (DataRow row in table.Rows)
            {
                var clinicId = row[0].ToStr();
                var clinicName = row[1].ToStr();

                if (string.IsNullOrWhiteSpace(clinicId))
                {
                    list.Add(string.Format(CustomerRes.ExcelClinicIdRequired, index));
                }

                if (string.IsNullOrWhiteSpace(clinicName))
                {
                    list.Add(string.Format(CustomerRes.ExcelClinicNameRequired, index));
                }

                ++index;
            }

            return list;
        }

        private List<string> CheckEmailValidFormat(DataTable table)
        {
            var list = new List<string>();

            var index = 2;

            foreach (DataRow row in table.Rows)
            {
                var clinicEmail = row[2].ToStr();
                var dentistEmail = row[5].ToStr();

                if (!string.IsNullOrWhiteSpace(clinicEmail) && !std.IsEmail(clinicEmail.Trim()))
                {
                    list.Add(string.Format(CustomerRes.ExcelEmailInvalid, index, clinicEmail));
                }

                if (!string.IsNullOrWhiteSpace(dentistEmail) && !std.IsEmail(dentistEmail.Trim()))
                {
                    list.Add(string.Format(CustomerRes.ExcelEmailInvalid, index, dentistEmail));
                }

                ++index;
            }

            return list;
        }

        private void PopulateSaveImportData(List<Customer> listCustomer, DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                var item = new Customer
                {
                    CustomerType = CustomerType,
                    ClinicId = row[0].ToStr(),
                    ClinicName = row[1].ToStr(),
                    ClinicEmail = row[2].ToStr(),
                    ClinicPhone = row[3].ToStr(),
                    DentistName = row[4].ToStr(),
                    DentistEmail = row[5].ToStr(),
                    DentistPhone = row[6].ToStr(),
                    CreatedDate = DateTime.UtcNow
                };

                item.InitId();

                listCustomer.Add(item);
            }
        }

        private DataTable LoadExcelFile(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToStr();
            var ef = new ExcelFile();
            if (extension.ToLower().EndsWith("xls"))
            {
                ef.LoadXls(fileName);
            }
            else if (extension.ToLower().EndsWith("csv"))
            {
                ef.LoadCsv(fileName, CsvType.CommaDelimited);
            }

            if (ef.Worksheets.Count == 0) return null;

            var ws = ef.Worksheets[0];

            if (ws.Rows.Count == 0) return null;

            var row = ws.Rows[0];

            var table = new DataTable();

            const int numCol = 6;

            for (var i = 0; i <= numCol; ++i)
            {
                var column = new DataColumn(row.Cells[i].Value.ToStr());
                table.Columns.Add(column);
            }

            for (var i = 1; i < ws.Rows.Count; ++i)
            {
                row = ws.Rows[i];
                var list = new List<object>();
                for (var colIndex = 0; colIndex <= numCol; ++colIndex)
                {
                    var cell = row.Cells[colIndex];
                    list.Add(cell.Value.ToStr());
                }

                table.Rows.Add(list.ToArray());
            }

            return table;
        }

        #endregion

        #region private

        protected void PopulateIndexModel(CustomerIndexModel model)
        {
            model.CustomerType = CustomerType;

            model.InitSortInfo();

            if (string.IsNullOrWhiteSpace(model.SortBy))
            {
                model.SortBy = "ClinicId";
            }

            var filter = new FindRequest
            {
                CustomerType = model.CustomerType,
                ClinicId = model.ClinicId,
                ClinicName = model.ClinicName,
                DentistName = model.DentistName,
                ClinicPhone = model.ClinicPhone,
                Address = model.Address,
                Email = model.Email,
                City = model.City,
                District = model.District,
                InterestingDevice = model.InterestingDevice,
                UsingRC = model.UsingRC,
                AssignTo = model.AssignTo,
                VisitFrom = model.VisitFrom.ToStartDateTimeNull(),
                VisitTo = model.VisitTo.ToEndDateTimeNull(),
                SortOption = new SortOption(new[] { new SortItem(model.SortBy, model.SortDirection.Value) })
            };

            if (!model.IsExport)
            {
                filter.PageOption = new PageOption
                {
                    PageSize = model.Pagination.PageSize,
                    PageNumber = model.Pagination.CurrentPageIndex
                };
            }

            var response = ServiceHelper.Customer.ExecuteDispose(s => s.FindCustomers(filter));
            model.Results = response.Results.MapList<CustomerModel>();
            model.Pagination.TotalRecords = response.TotalRecords;
        }

        protected void PopulateVisitIndexModel(CustomerModel model)
        {
            model.Visits.Where(i => i.IsNew).ForEach(i => i.InitId());
            model.VisitIndex.Results = model.Visits;
            model.VisitIndex.InitSortInfo();

            if (string.IsNullOrWhiteSpace(model.VisitIndex.SortBy))
            {
                model.VisitIndex.SortBy = "DateVisit";
            }

            var pageOption = new PageOption
            {
                PageSize = model.VisitIndex.Pagination.PageSize,
                PageNumber = model.VisitIndex.Pagination.CurrentPageIndex
            };

            if (model.VisitIndex.SortBy == "DateVisit")
            {
                if (model.VisitIndex.SortDirection.HasValue)
                {
                    model.VisitIndex.Results = model.VisitIndex.SortDirection.Value == SortDirection.Asc ?
                        model.VisitIndex.Results.OrderBy(i => i.DateVisit).ToList() :
                        model.VisitIndex.Results.OrderByDescending(i => i.DateVisit).ToList();
                }
                else
                {
                    model.VisitIndex.Results = model.VisitIndex.Results.OrderBy(i => i.DateVisit).ToList();
                }
            }

            if (pageOption.IsValid)
            {
                model.VisitIndex.Results =
                    model.VisitIndex.Results.Skip(pageOption.PageStartIndex).Take(pageOption.PageSize).ToList();
            }
        }

        protected void PopulateDataLogIndexModel(CustomerModel customerModel)
        {
            var model = customerModel.DataLogIndex;

            if (!model.SortDirection.HasValue)
            {
                model.SortDirection = SortDirection.Desc;
            }

            model.InitSortInfo();

            if (string.IsNullOrWhiteSpace(model.SortBy))
            {
                model.SortBy = "LogDate";
            }

            var filter = new Data.DataContract.DataLogDC.FindRequest
            {
                Table = TableLog.Customer,
                ItemId = customerModel.Id,
                SortOption = new SortOption(new[] { new SortItem(model.SortBy, model.SortDirection.Value) }),
                PageOption = new PageOption { PageSize = model.Pagination.PageSize, PageNumber = model.Pagination.CurrentPageIndex }
            };

            var response = ServiceHelper.DataLog.ExecuteDispose(s => s.FindDataLogs(filter));
            model.Results = response.Results.MapList<DataLogModel>();
            model.Pagination.TotalRecords = response.TotalRecords;
        }

        #endregion
    }
}