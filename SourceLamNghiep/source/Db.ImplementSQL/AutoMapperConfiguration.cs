using System;
using AutoMapper;
using Data.DataContract;
using Data.DataContract.AppSettingDC;
using Data.DataContract.ArticeDC;
using Data.DataContract.BeUserDC;
using Data.DataContract.CategoryDC;
using Data.DataContract.CustomerDC;
using Data.DataContract.DataLogDC;
using Data.DataContract.FileDC;
using Data.DataContract.HtmlContentDC;
using Data.DataContract.ProductDC;
using Data.DataContract.RefreshTokenDC;
using Data.DataContract.RequestPasswordDC;
using Data.DataContract.RoleDC;
using Data.DataContract.SystemEmailTemplateDC;

namespace Db.ImplementSQL
{
    public class AutoMapperConfiguration
    {
        #region Singleton

        private readonly static Lazy<AutoMapperConfiguration> LazyInstance = new Lazy<AutoMapperConfiguration>(() => new AutoMapperConfiguration());

        public static AutoMapperConfiguration Instance
        {
            get { return LazyInstance.Value; }
        }

        #endregion

        public void Configure()
        {
            ConfigureBeUser();
            ConfigureAppSetting();
            ConfigSystemEmailTemplate();
            ConfigRequestPassword();
            ConfigRolePermission();
            ConfigRole();
            ConfigHtmlContent();
            ConfigRefreshToken();
            ConfigureCustomer();
            ConfigProduct();
            ConfigCategories();
            ConfigFiles();
            ConfigArtice();
            ConfigDataLog();
        }

        private void ConfigDataLog()
        {
            Mapper.CreateMap<Entity.DataLog, DataLog>();
            Mapper.CreateMap<DataLog, Entity.DataLog>();
        }

        private void ConfigArtice()
        {
            Mapper.CreateMap<Entity.Artice, Artice>();
            Mapper.CreateMap<Artice, Entity.Artice>();
        }

        private void ConfigFiles()
        {
            Mapper.CreateMap<Entity.File, File>();
            Mapper.CreateMap<File, Entity.File>();
        }

        private void ConfigCategories()
        {
            Mapper.CreateMap<Entity.Category, Category>();
            Mapper.CreateMap<Category, Entity.Category>();

            Mapper.CreateMap<Entity.CategoryImage, CategoryImage>();
            Mapper.CreateMap<CategoryImage, Entity.CategoryImage>();
        }

        private void ConfigProduct()
        {
            Mapper.CreateMap<Entity.Product, Product>();
            Mapper.CreateMap<Product, Entity.Product>();

            Mapper.CreateMap<Entity.ProductImage, ProductImage>();
            Mapper.CreateMap<ProductImage, Entity.ProductImage>();
        }

        private void ConfigRefreshToken()
        {
            Mapper.CreateMap<Entity.RefreshToken, RefreshToken>();
            Mapper.CreateMap<RefreshToken, Entity.RefreshToken>();
        }

        private void ConfigHtmlContent()
        {
            Mapper.CreateMap<Entity.HtmlContent, HtmlContent>();
            Mapper.CreateMap<HtmlContent, Entity.HtmlContent>();
        }

        private void ConfigRole()
        {
            Mapper.CreateMap<Entity.Role, Role>();
            Mapper.CreateMap<Role, Entity.Role>();
            Mapper.CreateMap<Role, Reference>();
        }

        private void ConfigRolePermission()
        {
            Mapper.CreateMap<Entity.RolePermission, RolePermission>();
            Mapper.CreateMap<RolePermission, Entity.RolePermission>();
        }

        private void ConfigRequestPassword()
        {
            Mapper.CreateMap<Entity.RequestPassword, RequestPassword>();
            Mapper.CreateMap<RequestPassword, Entity.RequestPassword>();
        }

        private void ConfigSystemEmailTemplate()
        {
            Mapper.CreateMap<Entity.SystemEmailTemplate, SystemEmailTemplate>();
            Mapper.CreateMap<SystemEmailTemplate, Entity.SystemEmailTemplate>();
        }

        private void ConfigureAppSetting()
        {
            Mapper.CreateMap<Entity.AppSetting, AppSetting>();
            Mapper.CreateMap<AppSetting, Entity.AppSetting>();

            Mapper.CreateMap<AppSetting, Reference>();
        }

        private void ConfigureBeUser()
        {
            Mapper.CreateMap<Entity.BeUser, BeUser>();
            Mapper.CreateMap<BeUser, Entity.BeUser>();
        }

        private void ConfigureCustomer()
        {
            Mapper.CreateMap<Entity.Customer, Customer>();
            Mapper.CreateMap<Customer, Entity.Customer>();

            Mapper.CreateMap<Entity.CustomerImage, CustomerImage>();
            Mapper.CreateMap<CustomerImage, Entity.CustomerImage>();

            Mapper.CreateMap<Entity.CustomerVisit, CustomerVisit>();
            Mapper.CreateMap<CustomerVisit, Entity.CustomerVisit>();
        }
    }
}
