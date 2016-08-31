using AutoMapper;
using Data.DataContract;
using Data.DataContract.CategoryDC;
using Data.DataContract.ProductDC;
using Data.DataContract.RoleDC;
using Models.AppSetting;
using Models.Artice;
using Models.BeUser;
using Models.Category;
using Models.Customer;
using Models.DataLog;
using Models.File;
using Models.HtmlContent;
using Models.Product;
using Models.Role;
using Models.SystemEmailTemplate;
using System;
using Share.Helper.Models;

namespace Models
{
    public class AutoMapperCommonConfiguration
    {
        #region Singleton

        private readonly static Lazy<AutoMapperCommonConfiguration> LazyInstance = new Lazy<AutoMapperCommonConfiguration>(() => new AutoMapperCommonConfiguration());

        public static AutoMapperCommonConfiguration Instance
        {
            get { return LazyInstance.Value; }
        }

        #endregion

        public void Configure()
        {
            ConfigureBase();
            ConfigureAppSetting();
            ConfigSystemEmailTemplate();
            ConfigRole();
            ConfigureBeUser();
            ConfigureReference();
            ConfigHtmlContent();
            ConfigureCustomer();
            ConfigProduct();
            ConfigCategories();
            ConfigFiles();
            ConfigArtice();
            ConfigDataLog();
        }

        private void ConfigDataLog()
        {
            Mapper.CreateMap<DataLogModel, Data.DataContract.DataLogDC.DataLog>();
            Mapper.CreateMap<Data.DataContract.DataLogDC.DataLog, DataLogModel>();
        }

        private void ConfigArtice()
        {
            Mapper.CreateMap<ArticeModel, Data.DataContract.ArticeDC.Artice>();
            Mapper.CreateMap<Data.DataContract.ArticeDC.Artice, ArticeModel>();
        }

        private void ConfigFiles()
        {
            Mapper.CreateMap<FileModel, Data.DataContract.FileDC.File>();
            Mapper.CreateMap<Data.DataContract.FileDC.File, FileModel>();
        }

        private void ConfigCategories()
        {
            Mapper.CreateMap<CategoryModel, Data.DataContract.CategoryDC.Category>();
            Mapper.CreateMap<Data.DataContract.CategoryDC.Category, CategoryModel>();

            Mapper.CreateMap<CategoryImageModel, CategoryImage>();
            Mapper.CreateMap<CategoryImage, CategoryImageModel>();
        }

        private void ConfigProduct()
        {
            Mapper.CreateMap<ProductModel, Data.DataContract.ProductDC.Product>();
            Mapper.CreateMap<Data.DataContract.ProductDC.Product, ProductModel>();

            Mapper.CreateMap<ProductImageModel, ProductImage>();
            Mapper.CreateMap<ProductImage, ProductImageModel>();
        }

        private void ConfigHtmlContent()
        {
            Mapper.CreateMap<Data.DataContract.HtmlContentDC.HtmlContent, HtmlContentModel>();
            Mapper.CreateMap<HtmlContentModel, Data.DataContract.HtmlContentDC.HtmlContent>();
        }

        private void ConfigureReference()
        {
            Mapper.CreateMap<ReferenceModel, Reference>();
            Mapper.CreateMap<Reference, ReferenceModel>();
        }

        private void ConfigureBase()
        {
            Mapper.CreateMap<BaseModel, BaseDC>();
            Mapper.CreateMap<BaseDC, BaseModel>();
        }

        private void ConfigureAppSetting()
        {
            Mapper.CreateMap<Data.DataContract.AppSettingDC.AppSetting, AppSettingModel>();
            Mapper.CreateMap<AppSettingModel, Data.DataContract.AppSettingDC.AppSetting>();

            Mapper.CreateMap<Data.DataContract.AppSettingDC.AppSetting, ReferenceModel>();
        }

        private void ConfigSystemEmailTemplate()
        {
            Mapper.CreateMap<Data.DataContract.SystemEmailTemplateDC.SystemEmailTemplate, SystemEmailTemplateModel>();
            Mapper.CreateMap<SystemEmailTemplateModel, Data.DataContract.SystemEmailTemplateDC.SystemEmailTemplate>();
        }

        private void ConfigRole()
        {
            Mapper.CreateMap<RoleModel, Data.DataContract.RoleDC.Role>();
            Mapper.CreateMap<Data.DataContract.RoleDC.Role, RoleModel>();
            Mapper.CreateMap<Data.DataContract.RoleDC.Role, Reference>();

            Mapper.CreateMap<RolePermissionModel, RolePermission>();
            Mapper.CreateMap<RolePermission, RolePermissionModel>();
        }

        private void ConfigureBeUser()
        {
            Mapper.CreateMap<Data.DataContract.BeUserDC.BeUser, BeUserModel>();
            Mapper.CreateMap<BeUserModel, Data.DataContract.BeUserDC.BeUser>();
        }

        private void ConfigureCustomer()
        {
            Mapper.CreateMap<Data.DataContract.CustomerDC.Customer, CustomerModel>();
            Mapper.CreateMap<CustomerModel, Data.DataContract.CustomerDC.Customer>();

            Mapper.CreateMap<Data.DataContract.CustomerDC.CustomerImage, CustomerImageModel>();
            Mapper.CreateMap<CustomerImageModel, Data.DataContract.CustomerDC.CustomerImage>();

            Mapper.CreateMap<Data.DataContract.CustomerDC.CustomerVisit, CustomerVisitModel>();
            Mapper.CreateMap<CustomerVisitModel, Data.DataContract.CustomerDC.CustomerVisit>();
        }
    }
}